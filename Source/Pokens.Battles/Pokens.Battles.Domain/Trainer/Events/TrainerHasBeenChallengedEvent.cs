using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public class TrainerHasBeenChallengedEvent : IDomainEvent
    {
        private TrainerHasBeenChallengedEvent()
        {
        }

        public TrainerHasBeenChallengedEvent(Guid challengerId)
            : this()
        {
            ChallengerId = challengerId;
            ChallengedAt = DateTimeProvider.Instance().UtcNow;
        }

        public Guid ChallengerId { get; private set; }

        public DateTime ChallengedAt { get; private set; }
    }
}