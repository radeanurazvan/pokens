using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using EventStore.ClientAPI;
using MediatR;
using Newtonsoft.Json;
using Pomelo.Kernel.Common;

namespace Pomelo.Kernel.EventStore
{
    public abstract class EventStorePublisher
    {
        private readonly IEventStoreConnection storeConnection;
        private readonly IMediator mediator;
        private readonly EventStoreSettings settings;

        protected EventStorePublisher(IEventStoreConnection storeConnection, IMediator mediator, EventStoreSettings settings)
        {
            EnsureArg.IsNotNull(storeConnection);
            EnsureArg.IsNotNull(settings);
            this.storeConnection = storeConnection;
            this.mediator = mediator;
            this.settings = settings;
        }

        public Task<EventStoreAllCatchUpSubscription> StartAsync()
        {
            return this.Subscribe();
        }

        private async Task<EventStoreAllCatchUpSubscription> Subscribe()
        {
            var defaultSettings = CatchUpSubscriptionSettings.Default;
            var subscriptionSettings = new CatchUpSubscriptionSettings(defaultSettings.MaxLiveQueueSize, defaultSettings.ReadBatchSize, true, defaultSettings.ResolveLinkTos, GetType().Name);

            var checkpoint = await this.GetCheckpoint().Unwrap(c => c.LastProcessedPosition, AllCheckpoint.AllStart);
            return this.storeConnection.SubscribeToAllFrom(checkpoint, subscriptionSettings, OnEventAppeared, null, OnSubscriptionDropped);
        }

        private async Task OnEventAppeared(EventStoreCatchUpSubscription subscription, ResolvedEvent @event)
        {
            if (!@event.OriginalEvent.IsDomainEvent())
            {
                return;
            }

            var notification = this.GetNotificationMessage(@event.OriginalEvent);
            var genericPublish = mediator.GetType()
                .GetMethod(nameof(IMediator.Publish))
                .MakeGenericMethod(notification.GetType());

            await (Task) genericPublish.Invoke(this.mediator, new[] { notification });
            await this.StoreCheckpoint(@event);
        }

        private async void OnSubscriptionDropped(EventStoreCatchUpSubscription subscription, SubscriptionDropReason dropReason, Exception e)
        {
            await this.Subscribe();
        }

        private async Task<Maybe<CheckpointRegistered<Position>>> GetCheckpoint()
        {
            var checkpointReadOrNothing = await storeConnection.ReadStreamEventsBackwardAsync(
                this.CheckpointStream,
                StreamPosition.End,
                1,
                false,
                this.settings.Credentials).ToMaybe();

            return checkpointReadOrNothing.Where(cr => cr.Events.Any())
                .Select(cr => cr.Events[0])
                .Select(e => e.OriginalEvent.ToDecodedMessage<CheckpointRegistered<Position>>());
        }

        private async Task StoreCheckpoint(ResolvedEvent @event)
        {
            var metadata = StreamMetadata.Build().SetMaxCount(1);
            await storeConnection.SetStreamMetadataAsync(this.CheckpointStream, ExpectedVersion.Any, metadata, this.settings.Credentials);

            var checkpointMessage = new CheckpointRegistered<Position>(@event.OriginalPosition.GetValueOrDefault());
            await storeConnection.AppendToStreamAsync(this.CheckpointStream, ExpectedVersion.Any, checkpointMessage.ToEventData());
        }

        private object GetNotificationMessage(RecordedEvent @event)
        {
            var metadata = JsonConvert.DeserializeObject<EventMetadata>(@event.GetJsonMetadata());
            var eventType = Type.GetType(metadata.EventType);
            var notificationType = typeof(NotificationEvent<>).MakeGenericType(eventType);

            var message = JsonConvert.DeserializeObject(@event.GetJsonData(), eventType);

            return Activator.CreateInstance(notificationType, new[] { message, metadata.ToNotificationMetadata()});
        }

        private string CheckpointStream => $"{this.GetType().Name}-Checkpoint";
    }
}