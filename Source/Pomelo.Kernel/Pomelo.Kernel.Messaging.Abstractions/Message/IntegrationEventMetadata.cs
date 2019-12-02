using System;

namespace Pomelo.Kernel.Messaging.Abstractions
{
    public sealed class IntegrationEventMetadata
    {
        public IntegrationEventMetadata(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        public Guid AggregateId { get; private set; }
    }
}