using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public sealed class BattleHealthChangedEvent : IDomainEvent
    {
        private BattleHealthChangedEvent()
        {
        }

        public BattleHealthChangedEvent(Guid battleId, Guid trainerId, int newHealth)
            : this()
        {
            BattleId = battleId;
            TrainerId = trainerId;
            NewHealth = newHealth;
        }

        public Guid BattleId { get; private set; }

        public Guid TrainerId { get; private set; }

        public int NewHealth { get; private set; }
    }
}