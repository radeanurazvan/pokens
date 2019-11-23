using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Trainers.Domain
{
    internal class TrainerCreatedEvent : IDomainEvent
    {
        private TrainerCreatedEvent()
        {
        }

        public TrainerCreatedEvent(Trainer trainer) 
            : this()
        {
            Id = trainer.Id;
            Name = trainer.Name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }
}