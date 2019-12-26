using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerChallengedEvent : IDomainEvent
    {
        private TrainerChallengedEvent()
        {
        }

        public TrainerChallengedEvent(Guid trainerId)
            : this()
        {
            TrainerId = trainerId;
            ChallengedAt = DateTimeProvider.Instance().UtcNow;
        }

        public Guid TrainerId { get; private set; }

        public DateTime ChallengedAt { get; private set; }
    }
}