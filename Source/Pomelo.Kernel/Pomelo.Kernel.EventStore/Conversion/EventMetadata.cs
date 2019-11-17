using System;

namespace Pomelo.Kernel.EventStore
{
    internal sealed class EventMetadata
    {
        private EventMetadata()
        {
        }

        public EventMetadata(Guid aggregateId, Type eventType)
        {
            AggregateId = aggregateId;
            EventType = $"{eventType.FullName}, {eventType.Assembly.GetName().Name}";
        }

        public Guid AggregateId { get; private set; }

        public string EventType { get; private set; }
    }
}