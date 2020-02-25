using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerWonBattleEvent : IDomainEvent
    {
        private TrainerWonBattleEvent()
        {
        }

        public TrainerWonBattleEvent(Guid battleId)
            : this()
        {
            BattleId = battleId;
            WonAt = TimeProvider.Instance().UtcNow;
        }

        public Guid BattleId { get; private set; }

        public DateTime WonAt { get; private set; }
    }
}