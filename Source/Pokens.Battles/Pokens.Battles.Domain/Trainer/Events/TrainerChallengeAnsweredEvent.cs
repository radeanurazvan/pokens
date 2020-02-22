using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerChallengeAnsweredEvent : IDomainEvent
    {
        private TrainerChallengeAnsweredEvent()
        {
        }

        public static TrainerChallengeAnsweredEvent AcceptedFor(Guid challengeId)
        {
            return new TrainerChallengeAnsweredEvent
            {
                ChallengeId = challengeId,
                Accepted = true
            };
        }

        public Guid ChallengeId { get; private set; }

        public bool Accepted { get; private set; }

        public bool Rejected => !this.Accepted;
    }
}