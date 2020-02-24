using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerEnteredBattleEvent : IDomainEvent
    {
        private TrainerEnteredBattleEvent()
        {
        }

        public TrainerEnteredBattleEvent(Challenge challenge)
            : this()
        {
            ArenaId = challenge.ArenaId;
            ChallengeId = challenge.Id;
            TrainerId = challenge.ChallengedId;
            PokemonId = challenge.ChallengedPokemonId;
            EnemyId = challenge.ChallengerId;
            EnemyPokemonId = challenge.ChallengerPokemonId;
        }

        public Guid ArenaId { get; private set; }

        public Guid ChallengeId { get; private set; }

        public Guid TrainerId { get; private set; }

        public Guid PokemonId { get; private set; }

        public Guid EnemyPokemonId { get; private set; }

        public Guid EnemyId { get; private set; }
    }
}