using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    public sealed class TrainerCreatedEvent : IIntegrationEvent
    {
        private TrainerCreatedEvent()
        {
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }
}