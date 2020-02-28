using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class BattleHealthChangedEvent : IDomainEvent
    {
        private BattleHealthChangedEvent()
        {
        }

        public BattleHealthChangedEvent(Guid trainerId, int newHealth)
            : this()
        {
            TrainerId = trainerId;
            NewHealth = newHealth;
        }

        public Guid TrainerId { get; private set; }

        public int NewHealth { get; private set; }
    }
}