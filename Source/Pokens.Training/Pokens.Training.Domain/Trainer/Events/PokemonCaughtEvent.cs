using Pomelo.Kernel.Domain;

namespace Pokens.Training.Domain
{
    internal sealed class PokemonCaughtEvent : IDomainEvent
    {
        private PokemonCaughtEvent()
        {
        }

        public PokemonCaughtEvent(string pokemonId)
            : this()
        {
            PokemonId = pokemonId;
        }

        public string PokemonId { get; private set; }
    }
}