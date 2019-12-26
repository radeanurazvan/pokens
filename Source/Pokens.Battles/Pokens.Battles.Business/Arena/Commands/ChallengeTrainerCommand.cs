using System;
using Pomelo.Kernel.Common;

namespace Pokens.Battles.Business
{
    public sealed class ChallengeTrainerCommand : ICommand
    {
        public ChallengeTrainerCommand(Guid arenaId, Guid challengerId, Guid challengedId)
        {
            ArenaId = arenaId;
            ChallengerId = challengerId;
            ChallengedId = challengedId;
        }

        public Guid ArenaId { get; }

        public Guid ChallengerId { get; }

        public Guid ChallengedId { get; }
    }
}