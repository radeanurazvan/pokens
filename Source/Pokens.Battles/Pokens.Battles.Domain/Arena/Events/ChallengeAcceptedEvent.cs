using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class ChallengeAcceptedEvent : IDomainEvent
    {
        private ChallengeAcceptedEvent()
        {
        }

        public ChallengeAcceptedEvent(Guid challengeId)
            : this()
        {
            ChallengeId = challengeId;
        }

        public Guid ChallengeId { get; private set; }
    }
}