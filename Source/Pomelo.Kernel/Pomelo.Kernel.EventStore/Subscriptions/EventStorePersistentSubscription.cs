using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EventStore.ClientAPI;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Polly;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    public interface IEventStorePersistentSubscription 
    {
        IEventStorePersistentSubscription ForGroup(string group);
        Task Create();
        Task<EventStorePersistentSubscriptionBase> Connect();
    }

    public sealed class EventStorePersistentSubscription<TAggregate> : IEventStorePersistentSubscription
        where TAggregate : AggregateRoot
    {
        private readonly IEventStoreConnection connection;
        private readonly IStreamConfig<TAggregate> streamConfig;
        private readonly IServiceProvider provider;
        private readonly EventStoreEvents events;
        private readonly EventStoreSettings settings;

        public EventStorePersistentSubscription(
            IEventStoreConnection connection,
            IStreamConfig<TAggregate> streamConfig,
            IServiceProvider provider,
            EventStoreEvents events,
            EventStoreSettings settings)
        {
            this.connection = connection;
            this.streamConfig = streamConfig;
            this.provider = provider;
            this.events = events;
            this.settings = settings;
        }

        public string Group { get; private set; }

        public IEventStorePersistentSubscription ForGroup(string group)
        {
            this.Group = group;
            return this;
        }

        public async Task Create()
        {
            var checkpoint = await this.GetCheckpoint().Unwrap(c => c.LastProcessedPosition + 1, StreamPosition.Start);
            var subscriptionSettings = PersistentSubscriptionSettings.Create()
                .MinimumCheckPointCountOf(1)
                .StartFrom(checkpoint)
                .ResolveLinkTos()
                .Build();
            var retryPolicy = Policy.Handle<Exception>()
                .RetryAsync(3, (e, _) => events.RaiseCreateSubscriptionFailure(e));

            events.RaiseSubscriptionCreating<TAggregate>();
            await Policy.Handle<Exception>(e => e.Message.Contains("already exists"))
                .FallbackAsync(async _ =>
                {
                    await connection.UpdatePersistentSubscriptionAsync(streamConfig.GetCategoryStream(), Group, subscriptionSettings, settings.Credentials);
                    events.RaiseSubscriptionUpdated<TAggregate>(subscriptionSettings);
                })
                .WrapAsync(retryPolicy)
                .ExecuteAndCaptureAsync(async () =>
                {
                    await connection.CreatePersistentSubscriptionAsync(streamConfig.GetCategoryStream(), Group, subscriptionSettings, settings.Credentials);
                    events.RaiseSubscriptionCreated<TAggregate>();
                });
        }


        public async Task<EventStorePersistentSubscriptionBase> Connect()
        {
            var waitAndRetry = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(attempt), (e, _) => events.RaiseSubscriptionConnectionFailure(e));
            return await Policy<EventStorePersistentSubscriptionBase>.Handle<AggregateException>(e => e.InnerException != null && e.InnerException.Message.Contains("not found"))
                    .FallbackAsync(async _ =>
                    {
                        events.RaiseSubscriptionRecreating<TAggregate>();
                        await this.Create();
                        return await this.ConnectCore();
                    })
                    .WrapAsync(waitAndRetry)
                    .ExecuteAsync(ConnectCore);
        }

        private async Task<EventStorePersistentSubscriptionBase> ConnectCore()
        {
            var subscription = await connection.ConnectToPersistentSubscriptionAsync(streamConfig.GetCategoryStream(), Group, OnEventAppeared, OnSubscriptionDropped, settings.Credentials, autoAck: false);
            events.RaiseConnectedToSubscription(streamConfig.GetCategoryStream());
            return subscription;
        }

        private async Task OnEventAppeared(EventStorePersistentSubscriptionBase subscription, ResolvedEvent @event)
        {
            var notification = this.GetNotificationMessage(@event.Event);
            var genericPublish = typeof(IMediator)
                .GetMethods()
                .First(m => m.Name == nameof(IMediator.Publish) && m.IsGenericMethod)
                .MakeGenericMethod(notification.GetType());

            using (var scope = provider.CreateScope())
            {
                var waitAndRetry = Policy.Handle<Exception>()
                    .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(attempt), (e, _) => events.RaiseHandleNotificationFailed(e, notification));

                var mediator = scope.ServiceProvider.GetService<IMediator>();
                await Policy.Handle<Exception>()
                    .FallbackAsync(
                        _ => Task.Run(() => subscription.Fail(@event, PersistentSubscriptionNakEventAction.Park, "Message handle failed after 3 retries"), _),
                        e => Task.Run(() => events.RaiseNotificationNacked(e, notification)))
                    .WrapAsync(waitAndRetry)
                    .ExecuteAsync(async () =>
                    {
                        await (Task) genericPublish.Invoke(mediator, new[] {notification, CancellationToken.None});
                        await this.StoreCheckpoint(@event);
                        subscription.Acknowledge(@event);
                        events.RaiseNotificationHandled(notification);
                    });
            }
        }

        private async Task StoreCheckpoint(ResolvedEvent @event)
        {
            var metadata = StreamMetadata.Build().SetMaxCount(1);
            await connection.SetStreamMetadataAsync(this.CheckpointStream, ExpectedVersion.Any, metadata, this.settings.Credentials);

            var checkpointMessage = new CheckpointRegistered<long>(@event.OriginalEventNumber);
            await connection.AppendToStreamAsync(this.CheckpointStream, ExpectedVersion.Any, checkpointMessage.ToEventData());
        }

        private object GetNotificationMessage(RecordedEvent @event)
        {
            var metadata = JsonConvert.DeserializeObject<EventMetadata>(@event.GetJsonMetadata());
            var eventType = Type.GetType(metadata.EventType);
            var notificationType = typeof(NotificationEvent<>).MakeGenericType(eventType);

            var message = JsonConvert.DeserializeObject(@event.GetJsonData(), eventType);

            return Activator.CreateInstance(notificationType, message, metadata.ToNotificationMetadata());
        }

        private async void OnSubscriptionDropped(EventStorePersistentSubscriptionBase subscription, SubscriptionDropReason dropReason, Exception e)
        {
            await Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt * 2), (_, __) => events.RaiseSubscriptionDropped(dropReason, e))
                .ExecuteAndCaptureAsync(this.Connect);
        }

        private async Task<Maybe<CheckpointRegistered<long>>> GetCheckpoint()
        {
            var checkpointReadOrNothing = await connection.ReadStreamEventsBackwardAsync(
                this.CheckpointStream,
                StreamPosition.End,
                1,
                false,
                this.settings.Credentials).ToMaybe();

            return checkpointReadOrNothing.Where(cr => cr.Events.Any())
                .Select(cr => cr.Events[0])
                .Select(e => e.OriginalEvent.ToDecodedMessage<CheckpointRegistered<long>>());
        }

        private string CheckpointStream => $"{typeof(TAggregate).Name}PersistentCheckpoint";
    }
}