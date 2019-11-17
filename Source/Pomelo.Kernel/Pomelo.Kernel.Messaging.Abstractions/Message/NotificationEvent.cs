using MediatR;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.Messaging.Abstractions
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