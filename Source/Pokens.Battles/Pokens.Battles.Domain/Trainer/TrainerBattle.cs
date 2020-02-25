using System;
using CSharpFunctionalExtensions;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class TrainerBattle : Entity
    {
        private TrainerBattle()
        {
        }

        public static TrainerBattle Create(Guid challengeId, Guid enemyId, Guid pokemonId) => new TrainerBattle
        {
            Id = challengeId,
            Enemy = enemyId,
            Pokemon = pokemonId,
            StartedAt = TimeProvider.Instance().UtcNow
        };

        public Guid Pokemon { get; private set; }

        public Guid Enemy { get; private set; }

        public DateTime StartedAt { get; private set; }

        public Maybe<DateTime> EndedAt { get; private set; }
    }
}