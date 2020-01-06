using System;
using System.Text;
using CSharpFunctionalExtensions;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    internal static class Extensions
    {
        public static Maybe<T> ToDecodedMessage<T>(this RecordedEvent @event)
            where T : class => JsonConvert.DeserializeObject<T>(@event.GetJsonData());

        public static string GetJsonMetadata(this RecordedEvent @event) => Encoding.UTF8.GetString(@event.Metadata);

        public static string GetJsonData(this RecordedEvent @event) => Encoding.UTF8.GetString(@event.Data);

        public static bool IsDomainEvent(this RecordedEvent @event)
        {
            if (@event.EventStreamId.StartsWith("$"))
            {
                return false;
            }

            return Result.Try(() => JsonConvert.DeserializeObject<EventMetadata>(@event.GetJsonMetadata()))
                .Ensure(m => m != null, "Event metadata cannot be null")
                .Map(m => Type.GetType(m.EventType))
                .Ensure(t => typeof(IDomainEvent).IsAssignableFrom(t), "Event is not domain event")
                .Ensure(t => t != typeof(CheckpointRegistered<>), "Checkpoint message is not domain event")
                .Finally(x => !x.IsFailure);
        }

        public static NotificationEventMetadata ToNotificationMetadata(this EventMetadata metadata)
        {
            return new NotificationEventMetadata(metadata.AggregateId);
        }
    }
}