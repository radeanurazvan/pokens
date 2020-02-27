using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class ChallengeInArena : Entity
    {
        private ChallengeInArena()
        {
        }

        public ChallengeInArena(Guid id, Guid challengerId, Guid challengedId)
            : this()
        {
            Id = id;
            ChallengerId = challengerId;
            ChallengedId = challengedId;
        }

        public Guid ChallengerId { get; private set; }

        public Guid ChallengedId { get; private set; }
    }
}