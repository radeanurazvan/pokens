using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class ChallengeOccurredEvent : IDomainEvent
    {
        private ChallengeOccurredEvent()
        {
        }

        public ChallengeOccurredEvent(Guid id, Guid challengerId, Guid challengedId)
            : this()
        {
            Id = id;
            ChallengerId = challengerId;
            ChallengedId = challengedId;
        }

        public Guid Id { get; private set; }

        public Guid ChallengerId { get; private set; }

        public Guid ChallengedId { get; private set; }
    }
}