using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using MediatR;
using Pokens.Battles.Domain;
using Pomelo.Kernel.EventStore;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Battles.Reactors.Publishers
{
    internal sealed class ArenaPublisher : INotificationHandler<NotificationEvent<TrainerAcceptedChallengeEvent>>
    {
        private readonly IMessageBus bus;

        public ArenaPublisher(IMessageBus bus)
        {
            this.bus = bus;
        }

        public Task Handle(NotificationEvent<TrainerAcceptedChallengeEvent> notification, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(notification);
            return this.bus.Publish(notification.Data);
        }
    }
}