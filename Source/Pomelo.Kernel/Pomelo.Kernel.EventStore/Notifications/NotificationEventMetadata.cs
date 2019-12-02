using System;

namespace Pomelo.Kernel.EventStore
{
    public sealed class NotificationEventMetadata
    {
        public NotificationEventMetadata(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        public Guid AggregateId { get; private set; }
    }
}