using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Domain
{
    internal sealed class PokemonLeveledUpEvent : IDomainEvent
    {
        private PokemonLeveledUpEvent()
        {
        }

        public PokemonLeveledUpEvent(Pokemon pokemon)
            : this()
        {
            PokemonId = pokemon.Id;
            Level = pokemon.Level;
        }

        public string PokemonId { get; private set; }

        public int Level { get; private set; }
    }
}