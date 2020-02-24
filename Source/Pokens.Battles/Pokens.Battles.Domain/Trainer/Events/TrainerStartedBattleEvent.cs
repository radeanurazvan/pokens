using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerStartedBattleEvent : IDomainEvent
    {
        private TrainerStartedBattleEvent()
        {
        }

        public TrainerStartedBattleEvent(Challenge challenge)
            : this()
        {
            ArenaId = challenge.ArenaId;
            ChallengeId = challenge.Id;
            TrainerId = challenge.ChallengerId;
            PokemonId = challenge.ChallengerPokemonId;
            EnemyId = challenge.ChallengedId;
            EnemyPokemonId = challenge.ChallengedPokemonId;
        }

        public Guid ArenaId { get; private set; }

        public Guid ChallengeId { get; private set; }

        public Guid TrainerId { get; private set; }

        public Guid PokemonId { get; private set; }

        public Guid EnemyPokemonId { get; private set; }

        public Guid EnemyId { get; private set; }
    }
}