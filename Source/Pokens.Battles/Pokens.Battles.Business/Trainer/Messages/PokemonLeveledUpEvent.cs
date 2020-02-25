using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    public sealed class PokemonLeveledUpEvent : IIntegrationEvent
    {
        private PokemonLeveledUpEvent()
        {
        }

        public Guid PokemonId { get; private set; }

        public int Level { get; private set; }
    }
}