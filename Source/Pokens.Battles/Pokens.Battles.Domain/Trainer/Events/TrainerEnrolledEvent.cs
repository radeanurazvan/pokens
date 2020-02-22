using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerEnrolledEvent : IDomainEvent
    {
        private TrainerEnrolledEvent()
        {
        }

        public TrainerEnrolledEvent(Guid arenaId)
            : this()
        {
            ArenaId = arenaId;
        }

        public Guid ArenaId { get; private set; }
    }
}