using Pomelo.Kernel.Domain;

namespace Pokens.Training.Domain
{
    internal sealed class StarterPokemonChosenEvent : IDomainEvent
    {
        private StarterPokemonChosenEvent()
        {
        }

        public StarterPokemonChosenEvent(string pokemonId)
            : this()
        {
            PokemonId = pokemonId;
        }

        public string PokemonId { get; private set; }
    }
}