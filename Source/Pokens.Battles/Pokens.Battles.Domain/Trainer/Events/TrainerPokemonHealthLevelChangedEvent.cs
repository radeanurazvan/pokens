using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerPokemonHealthLevelChangedEvent : IDomainEvent
    {
        private TrainerPokemonHealthLevelChangedEvent()
        {
        }

        public TrainerPokemonHealthLevelChangedEvent(Guid pokemonId, int level)
            : this()
        {
            PokemonId = pokemonId;
            BonusHealth = PokemonHealth.GetBonusHealth(level);
        }

        public Guid PokemonId { get; private set; }

        public int BonusHealth { get; private set; }
    }
}