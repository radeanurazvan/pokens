using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Battles.Business
{
    public sealed class TrainerCreatedEvent : IDomainEvent, IBusMessage
    {
        private TrainerCreatedEvent()
        {
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }
}