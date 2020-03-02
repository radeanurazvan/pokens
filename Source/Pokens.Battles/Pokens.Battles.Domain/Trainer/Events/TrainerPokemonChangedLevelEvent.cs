using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerPokemonChangedLevelEvent : IDomainEvent
    {
        private TrainerPokemonChangedLevelEvent()
        {
        }

        public TrainerPokemonChangedLevelEvent(Guid pokemonId, int level)    
            : this()
        {
            PokemonId = pokemonId;
            Level = level;
        }

        public Guid PokemonId { get; private set; }

        public int Level { get; private set; }
    }
}