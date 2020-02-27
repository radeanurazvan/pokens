using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerChallengeGotAnsweredEvent : IDomainEvent
    {
        private TrainerChallengeGotAnsweredEvent()
        {
        }

        public static TrainerChallengeGotAnsweredEvent AcceptedFor(Guid challengeId)
        {
            return new TrainerChallengeGotAnsweredEvent
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