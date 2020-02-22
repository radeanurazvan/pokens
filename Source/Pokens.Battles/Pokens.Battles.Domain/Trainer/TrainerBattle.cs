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

        public static TrainerBattle Against(Guid enemy) => new TrainerBattle
        {
            Enemy = enemy,
            StartedAt = TimeProvider.Instance().UtcNow
        };

        public Guid Enemy { get; private set; }

        public DateTime StartedAt { get; private set; }

        public Maybe<DateTime> EndedAt { get; private set; }
    }
}