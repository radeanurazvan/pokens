using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerAcceptedChallengeEvent : IDomainEvent
    {
        private TrainerAcceptedChallengeEvent()
        {
        }

        public TrainerAcceptedChallengeEvent(Guid challengeId)
             : this()
        {
            ChallengeId = challengeId;
        }

        public Guid ChallengeId { get; private set; }
    }
}