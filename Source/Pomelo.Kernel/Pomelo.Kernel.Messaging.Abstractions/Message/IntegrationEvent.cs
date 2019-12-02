using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.Messaging.Abstractions
{
    public sealed class IntegrationEvent<T> : IBusMessage
        where T : IDomainEvent
    {
        public IntegrationEvent(T data, IntegrationEventMetadata metadata)
        {
            Data = data;
            Metadata = metadata;
        }

        public T Data { get; }

        public IntegrationEventMetadata Metadata { get; }
    }
}