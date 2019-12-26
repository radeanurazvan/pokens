using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    internal sealed class ChallengeOccurredEvent : IDomainEvent
    {
        private ChallengeOccurredEvent()
        {
        }

        public ChallengeOccurredEvent(Guid challengerId, Guid challengedId)
            : this()
        {
            ChallengerId = challengerId;
            ChallengedId = challengedId;
        }

        public Guid ChallengerId { get; private set; }

        public Guid ChallengedId { get; private set; }
    }
}