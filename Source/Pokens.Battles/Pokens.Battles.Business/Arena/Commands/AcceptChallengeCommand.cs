using System;
using Pomelo.Kernel.Common;

namespace Pokens.Battles.Business
{
    public sealed class AcceptChallengeCommand : ICommand
    {
        public AcceptChallengeCommand(Guid arenaId, Guid challengeId)
        {
            ArenaId = arenaId;
            ChallengeId = challengeId;
        }

        public Guid ArenaId { get; }

        public Guid ChallengeId { get; }
    }
}