using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Pokens.Battles.Domain;
using Pokens.Battles.Read.Domain;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.DataSynchronization;
using Pomelo.Kernel.EventStore;

namespace Pokens.Battles.Reactors.Denormalizers
{
    internal sealed class BattleDenormalizer : INotificationHandler<NotificationEvent<BattleStartedEvent>>,
        INotificationHandler<NotificationEvent<BattleHealthChangedEvent>>,
        INotificationHandler<NotificationEvent<BattleEndedEvent>>,
        INotificationHandler<NotificationEvent<PlayerTookTurnEvent>>,
        INotificationHandler<NotificationEvent<PlayerUsedAbilityEvent>>,
        INotificationHandler<NotificationEvent<PokemonDodgedAbility>>
    {
        private readonly ISyncReadRepository<TrainerModel> trainersRead;
        private readonly ISyncStorage storage;

        public BattleDenormalizer(ISyncStorage storage, ISyncReadRepository<TrainerModel> trainersRead)
        {
            this.storage = storage;
            this.trainersRead = trainersRead;
        }

        public async Task Handle(NotificationEvent<BattleStartedEvent> notification, CancellationToken cancellationToken)
        {
            var challenge = (await trainersRead.GetById(notification.Data.AttackerId.ToString()))
                .Select(a => a.Challenges)
                .Bind(list => list.FirstOrNothing(c => c.Id == notification.Data.Id.ToString()))
                .Value;

            await storage.Create(new BattleModel
            {
                Id = notification.Data.Id.ToString(),
                ArenaId = notification.Data.ArenaId.ToString(),
                AttackerId = notification.Data.AttackerId.ToString(),
                AttackerPokemonId = challenge.PokemonId,
                AttackerHealth = notification.Data.AttackerPokemon.Health,
                DefenderId = notification.Data.DefenderId.ToString(),
                DefenderPokemonId = challenge.EnemyPokemonId,
                DefenderHealth = notification.Data.DefenderPokemon.Health,
                StartedAt = notification.Data.StartedAt
            });
        }

        public Task Handle(NotificationEvent<BattleHealthChangedEvent> notification, CancellationToken cancellationToken)
        {
            return storage.Update<BattleModel>(notification.Metadata.AggregateId.ToString(), b =>
            {
                if (notification.Data.TrainerId.ToString() == b.AttackerId)
                {
                    b.AttackerHealth = notification.Data.NewHealth;
                    b.Commentaries.Add($"Attacker's health changed to {notification.Data.NewHealth}");
                }
                else
                {
                    b.DefenderHealth = notification.Data.NewHealth;
                    b.Commentaries.Add($"Defender's health changed to {notification.Data.NewHealth}");
                }
            });
        }

        public Task Handle(NotificationEvent<BattleEndedEvent> notification, CancellationToken cancellationToken)
        {
            return storage.Update<BattleModel>(notification.Metadata.AggregateId.ToString(), b =>
            {
                b.Commentaries.Add("The battle has ended!");
                b.Commentaries.Add(notification.Data.Winner.ToString() == b.AttackerId ? "Attacker won!" : "Defender won!");
                b.EndedAt = notification.Data.EndedAt;
            });
        }

        public Task Handle(NotificationEvent<PlayerTookTurnEvent> notification, CancellationToken cancellationToken)
        {
            return storage.Update<BattleModel>(notification.Metadata.AggregateId.ToString(), b =>
            {
                b.Commentaries.Add(notification.Data.PlayerId.ToString() == b.AttackerId
                    ? "Attacker's took his turn."
                    : "Defender's took his turn.");
            });
        }

        public Task Handle(NotificationEvent<PlayerUsedAbilityEvent> notification, CancellationToken cancellationToken)
        {
            return storage.Update<BattleModel>(notification.Metadata.AggregateId.ToString(), b =>
            {
                b.Commentaries.Add(notification.Data.PlayerId.ToString() == b.AttackerId
                    ? $"Attacker used {notification.Data.Ability.Name}, dealing {notification.Data.DamageDealt} damage!"
                    : $"Defender used {notification.Data.Ability.Name}, dealing {notification.Data.DamageDealt} damage!");
            });
        }

        public Task Handle(NotificationEvent<PokemonDodgedAbility> notification, CancellationToken cancellationToken)
        {
            return storage.Update<BattleModel>(notification.Metadata.AggregateId.ToString(), b => b.Commentaries.Add("Woah, the pokemon dodged the attack!"));
        }
    }
}