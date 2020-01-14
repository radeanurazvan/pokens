using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class ChallengeInArena : Entity
    {
        private ChallengeInArena()
        {
        }

        public ChallengeInArena(Guid challengerId, Guid challengedId)
            : this()
        {
            ChallengerId = challengerId;
            ChallengedId = challengedId;
        }

        public Guid ChallengerId { get; private set; }

        public Guid ChallengedId { get; private set; }
    }
}