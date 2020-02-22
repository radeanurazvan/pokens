using System;
using Pomelo.Kernel.Events.Abstractions;

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