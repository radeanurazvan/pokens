using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pokens.Battles.Domain;
using Pokens.Battles.Read.Domain;
using Pomelo.Kernel.DataSynchronization;
using Pomelo.Kernel.EventStore;

namespace Pokens.Battles.Reactors.Denormalizers
{
    internal sealed class TrainerDenormalizer : INotificationHandler<NotificationEvent<TrainerRegisteredEvent>>,
        INotificationHandler<NotificationEvent<TrainerChallengedEvent>>,
        INotificationHandler<NotificationEvent<TrainerHasBeenChallengedEvent>>,
        INotificationHandler<NotificationEvent<TrainerAcceptedChallengeEvent>>,
        INotificationHandler<NotificationEvent<TrainerChallengeGotAnsweredEvent>>
    {
        private readonly ISyncStorage storage;

        public TrainerDenormalizer(ISyncStorage storage)
        {
            this.storage = storage;
        }

        public Task Handle(NotificationEvent<TrainerRegisteredEvent> notification, CancellationToken cancellationToken)
        {
            return storage.Create(new TrainerModel
            {
                Id = notification.Metadata.AggregateId.ToString(),
                Name = notification.Data.Name
            });
        }

        public Task Handle(NotificationEvent<TrainerChallengedEvent> notification, CancellationToken cancellationToken)
        {
            var challenge = new ChallengeModel
            {
                Id = notification.Data.ChallengeId.ToString(),
                EnemyId = notification.Data.TrainerId.ToString(),
                Enemy = notification.Data.TrainerName,
                PokemonId = notification.Data.PokemonId.ToString(),
                Pokemon = notification.Data.PokemonName,
                EnemyPokemonId = notification.Data.ChallengedPokemonId.ToString(),
                EnemyPokemon = notification.Data.ChallengedPokemonName,
                ChallengerId = notification.Metadata.AggregateId.ToString(),
                ChallengedAt = notification.Data.ChallengedAt,
                ArenaId = notification.Data.ArenaId.ToString(),
                Status = "Pending"
            };
            return storage.Update<TrainerModel>(notification.Metadata.AggregateId.ToString(), t => t.Challenges.Add(challenge));
        }

        public Task Handle(NotificationEvent<TrainerHasBeenChallengedEvent> notification, CancellationToken cancellationToken)
        {
            var challenge = new ChallengeModel
            {
                Id = notification.Data.ChallengeId.ToString(),
                EnemyId = notification.Data.ChallengerId.ToString(),
                Enemy = notification.Data.ChallengerName,
                PokemonId = notification.Data.PokemonId.ToString(),
                Pokemon = notification.Data.Pokemon,
                EnemyPokemonId = notification.Data.ChallengerPokemonId.ToString(),
                EnemyPokemon = notification.Data.ChallengerPokemonName,
                ChallengerId = notification.Data.ChallengerId.ToString(),
                ChallengedAt = notification.Data.ChallengedAt,
                ArenaId = notification.Data.ArenaId.ToString(),
                Status = "Pending"
            };
            return storage.Update<TrainerModel>(notification.Metadata.AggregateId.ToString(), t => t.Challenges.Add(challenge));
        }

        public Task Handle(NotificationEvent<TrainerAcceptedChallengeEvent> notification, CancellationToken cancellationToken)
        {
            return storage.Update<TrainerModel>(notification.Metadata.AggregateId.ToString(), t => t.AcceptedChallenge(notification.Data.ChallengeId.ToString()));
        }

        public Task Handle(NotificationEvent<TrainerChallengeGotAnsweredEvent> notification, CancellationToken cancellationToken)
        {
            return storage.Update<TrainerModel>(notification.Metadata.AggregateId.ToString(), t => t.ChallengeGotAnswered(notification.Data.ChallengeId.ToString(), notification.Data.Accepted));
        }
    }
}