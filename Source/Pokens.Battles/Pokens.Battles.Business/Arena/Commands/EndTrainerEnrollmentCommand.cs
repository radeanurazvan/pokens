using System;
using Pomelo.Kernel.Common;

namespace Pokens.Battles.Business
{
    public sealed class EndTrainerEnrollmentCommand : ICommand
    {
        public EndTrainerEnrollmentCommand(Guid arenaId, Guid trainerId)
        {
            ArenaId = arenaId;
            TrainerId = trainerId;
        }

        public Guid ArenaId { get; }

        public Guid TrainerId { get; }
    }
}