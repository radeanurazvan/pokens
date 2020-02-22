using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerEnteredBattleEvent : IDomainEvent
    {
        private TrainerEnteredBattleEvent()
        {
        }

        public TrainerEnteredBattleEvent(Guid enemy)
            : this()
        {
            Enemy = enemy;
        }

        public Guid Enemy { get; private set; }
    }
}