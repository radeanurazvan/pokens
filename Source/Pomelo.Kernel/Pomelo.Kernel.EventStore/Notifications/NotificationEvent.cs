using MediatR;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pomelo.Kernel.EventStore
{
    public sealed class NotificationEvent<T> : IBusMessage, INotification
        where T : IDomainEvent
    {
        public NotificationEvent(T data, NotificationEventMetadata metadata)
        {
            Data = data;
            Metadata = metadata;
        }

        public T Data { get; }

        public NotificationEventMetadata Metadata { get; }
    }
}