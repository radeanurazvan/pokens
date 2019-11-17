using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pomelo.Kernel.EventStore
{
    public abstract class EventStorePublisher
    {
        private readonly IEventStoreConnection storeConnection;
        private readonly IMessageBus bus;
        private readonly EventStoreSettings settings;

        protected EventStorePublisher(IEventStoreConnection storeConnection, IMessageBus bus, EventStoreSettings settings)
        {
            EnsureArg.IsNotNull(storeConnection);
            EnsureArg.IsNotNull(bus);
            EnsureArg.IsNotNull(settings);
            this.storeConnection = storeConnection;
            this.bus = bus;
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
            var genericPublish = bus.GetType()
                .GetMethod(nameof(IMessageBus.Publish))
                .MakeGenericMethod(notification.GetType());

            await (Task) genericPublish.Invoke(this.bus, new[] { notification });
            await this.StoreCheckpoint(@event);
        }

        private async void OnSubscriptionDropped(EventStoreCatchUpSubscription subscription, SubscriptionDropReason dropReason, Exception e)
        {
            await this.Subscribe();
        }

        private async Task<Maybe<CheckpointRegistered>> GetCheckpoint()
        {
            var checkpointReadOrNothing = await storeConnection.ReadStreamEventsBackwardAsync(
                this.CheckpointStream,
                StreamPosition.End,
                1,
                false,
                this.settings.Credentials).ToMaybe();

            return checkpointReadOrNothing.Where(cr => cr.Events.Any())
                .Select(cr => cr.Events[0])
                .Select(e => e.OriginalEvent.ToDecodedMessage<CheckpointRegistered>());
        }

        private async Task StoreCheckpoint(ResolvedEvent @event)
        {
            var metadata = StreamMetadata.Build().SetMaxCount(1);
            await storeConnection.SetStreamMetadataAsync(this.CheckpointStream, ExpectedVersion.Any, metadata, this.settings.Credentials);

            var checkpointMessage = new CheckpointRegistered(@event.OriginalPosition.GetValueOrDefault());
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