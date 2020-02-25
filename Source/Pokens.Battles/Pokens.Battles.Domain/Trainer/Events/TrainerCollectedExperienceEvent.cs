using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerCollectedExperienceEvent : IDomainEvent
    {
        private TrainerCollectedExperienceEvent()
        {
        }

        public TrainerCollectedExperienceEvent(Guid pokemonId, int amount)  
            : this()
        {
            PokemonId = pokemonId;
            Amount = amount;
        }

        public Guid PokemonId { get; private set; }

        public int Amount { get; private set; }
    }
}