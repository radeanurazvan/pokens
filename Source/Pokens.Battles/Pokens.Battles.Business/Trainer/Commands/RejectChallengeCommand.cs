using System;
using Pomelo.Kernel.Common;

namespace Pokens.Battles.Business
{
    public sealed class RejectChallengeCommand : ICommand
    {
        public RejectChallengeCommand(Guid trainerId, Guid challengeId)
        {
            TrainerId = trainerId;
            ChallengeId = challengeId;
        }

        public Guid TrainerId { get; }

        public Guid ChallengeId { get; }
    }
}