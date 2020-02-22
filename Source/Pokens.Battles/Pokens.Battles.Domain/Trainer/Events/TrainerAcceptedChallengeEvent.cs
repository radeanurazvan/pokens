using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public sealed class TrainerAcceptedChallengeEvent : IDomainEvent
    {
        private TrainerAcceptedChallengeEvent()
        {
        }

        public TrainerAcceptedChallengeEvent(Guid arenaId, Guid challengeId)
             : this()
        {
            ArenaId = arenaId;
            ChallengeId = challengeId;
        }

        public Guid ArenaId { get; private set; }

        public Guid ChallengeId { get; private set; }
    }
}