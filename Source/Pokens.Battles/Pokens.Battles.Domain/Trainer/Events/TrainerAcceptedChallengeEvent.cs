using System;
using MediatR;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Battles.Domain
{
    public sealed class TrainerAcceptedChallengeEvent : IDomainEvent, IBusMessage
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