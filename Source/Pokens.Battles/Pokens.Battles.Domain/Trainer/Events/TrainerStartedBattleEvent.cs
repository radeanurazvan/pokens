using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerStartedBattleEvent : IDomainEvent
    {
        private TrainerStartedBattleEvent()
        {
        }

        public TrainerStartedBattleEvent(Guid enemy)
            : this()
        {
            Enemy = enemy;
        }

        public Guid Enemy { get; private set; }
    }
}