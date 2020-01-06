using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using Pokens.Battles.Domain;
using Pokens.Battles.Read.Domain;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.DataSynchronization;
using Pomelo.Kernel.EventStore;

namespace Pokens.Battles.Reactors.Denormalizers
{
    internal sealed class ArenaDenormalizer : INotificationHandler<NotificationEvent<ArenaOpenedEvent>>,
        INotificationHandler<NotificationEvent<ArenaEnrolledTrainerEvent>>,
        INotificationHandler<NotificationEvent<ArenaEnrollmentEndedEvent>>
    {
        private readonly ISyncStorage storage;

        public ArenaDenormalizer(ISyncStorage storage)
        {
            this.storage = storage;
        }

        public Task Handle(NotificationEvent<ArenaOpenedEvent> notification, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(notification);
            return storage.Create(new ArenaModel
            {
                Id = notification.Data.Id.ToString(),
                Name = notification.Data.Name,
                RequiredLevel = notification.Data.RequiredLevel
            });
        }

        public Task Handle(NotificationEvent<ArenaEnrolledTrainerEvent> notification, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(notification);
            return storage.Update<ArenaModel>(notification.Metadata.AggregateId.ToString(), a => a.Trainers.Add(new ArenaTrainerModel
            {
                Id = notification.Data.TrainerId.ToString(),
                Name = notification.Data.Name,
                JoinedAt = notification.Data.JoinedAt
            }));
        }

        public Task Handle(NotificationEvent<ArenaEnrollmentEndedEvent> notification, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(notification);
            return storage.Update<ArenaModel>(notification.Metadata.AggregateId.ToString(), a => a.Trainers.FirstOrNothing(t => t.Id == notification.Data.TrainerId.ToString())
                .Execute(t => a.Trainers.Remove(t)));
        }
    }
}