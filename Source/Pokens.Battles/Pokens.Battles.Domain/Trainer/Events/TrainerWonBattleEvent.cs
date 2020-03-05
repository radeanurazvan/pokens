using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public sealed class TrainerWonBattleEvent : IDomainEvent
    {
        private TrainerWonBattleEvent()
        {
        }

        public TrainerWonBattleEvent(Guid trainerId, Guid battleId, int experience)
            : this()
        {
            TrainerId = trainerId;
            Experience = experience;
            BattleId = battleId;
            WonAt = TimeProvider.Instance().UtcNow;
        }

        public Guid TrainerId { get; private set; }

        public int Experience { get; private set; }

        public Guid BattleId { get; private set; }

        public DateTime WonAt { get; private set; }
    }
}