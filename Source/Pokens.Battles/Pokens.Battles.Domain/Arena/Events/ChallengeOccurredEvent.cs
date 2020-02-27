using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class ChallengeOccurredEvent : IDomainEvent
    {
        private ChallengeOccurredEvent()
        {
        }

        public ChallengeOccurredEvent(ChallengeInArena challenge)
            : this()
        {
            Id = challenge.Id;
            ChallengerId = challenge.ChallengerId;
            ChallengedId = challenge.ChallengedId;
        }

        public Guid Id { get; private set; }

        public Guid ChallengerId { get; private set; }

        public Guid ChallengedId { get; private set; }
    }
}