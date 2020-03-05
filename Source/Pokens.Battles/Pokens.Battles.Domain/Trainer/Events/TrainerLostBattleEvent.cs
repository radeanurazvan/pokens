using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public sealed class TrainerLostBattleEvent : IDomainEvent
    {
        private TrainerLostBattleEvent()
        {
        }

        public TrainerLostBattleEvent(Guid trainerId, Guid battleId, int experience)
            : this()
        {
            TrainerId = trainerId;
            Experience = experience;
            BattleId = battleId;
            LostAt = TimeProvider.Instance().UtcNow;
        }

        public Guid TrainerId { get; private set; }

        public int Experience { get; private set; }

        public Guid BattleId { get; private set; }

        public DateTime LostAt { get; private set; }
    }
}