using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    internal sealed class ArenaOpenedEvent : IDomainEvent
    {
        private ArenaOpenedEvent()
        {
        }

        public ArenaOpenedEvent(Guid id, string name, int requiredLevel)
            : this()
        {
            Id = id;
            Name = name;
            RequiredLevel = requiredLevel;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int RequiredLevel { get; private set; }
    }
}