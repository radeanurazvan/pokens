using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class ArenaEnrollmentEndedEvent : IDomainEvent
    {
        private ArenaEnrollmentEndedEvent()
        {
        }

        public ArenaEnrollmentEndedEvent(Guid trainerId)
            : this()
        {
            TrainerId = trainerId;
        }

        public Guid TrainerId { get; private set; }
    }
}