using System;
using Pomelo.Kernel.Common;

namespace Pokens.Battles.Business
{
    public sealed class EnrollTrainerCommand : ICommand
    {
        public EnrollTrainerCommand(Guid arenaId, Guid trainerId)
        {
            ArenaId = arenaId;
            TrainerId = trainerId;
        }

        public Guid ArenaId { get; }
        
        public Guid TrainerId { get; }
    }
}