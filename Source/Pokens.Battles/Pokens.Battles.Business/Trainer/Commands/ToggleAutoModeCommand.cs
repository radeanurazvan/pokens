using System;
using Pomelo.Kernel.Common;

namespace Pokens.Battles.Business
{
    public sealed class ToggleAutoModeCommand : ICommand
    {
        public ToggleAutoModeCommand(Guid trainerId)
        {
            TrainerId = trainerId;
        }

        public Guid TrainerId { get; }
    }
}