using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerLostBattleEvent : IDomainEvent
    {
        private TrainerLostBattleEvent()
        {
        }

        public TrainerLostBattleEvent(Guid battleId, int experience)
            : this()
        {
            Experience = experience;
            BattleId = battleId;
            LostAt = TimeProvider.Instance().UtcNow;
        }

        public int Experience { get; private set; }

        public Guid BattleId { get; private set; }

        public DateTime LostAt { get; private set; }
    }
}