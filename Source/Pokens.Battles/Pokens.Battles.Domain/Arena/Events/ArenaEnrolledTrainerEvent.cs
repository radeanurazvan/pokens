using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    internal sealed class ArenaEnrolledTrainerEvent : IDomainEvent
    {
        private ArenaEnrolledTrainerEvent()
        {
        }

        public ArenaEnrolledTrainerEvent(Trainer trainer)
            : this()
        {
            TrainerId = trainer.Id;
            Name = trainer.Name;
            JoinedAt = DateTimeProvider.Instance().UtcNow;
        }

        public Guid TrainerId { get; private set; }

        public string Name { get; private set; }

        public DateTime JoinedAt { get; private set; }
    }
}