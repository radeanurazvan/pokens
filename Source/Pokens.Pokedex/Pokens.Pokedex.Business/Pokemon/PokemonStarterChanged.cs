using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Pokedex.Business
{
    internal sealed class PokemonStarterChanged : IDomainEvent
    {
        private PokemonStarterChanged()
        {

        }

        public PokemonStarterChanged(Pokemon pokemon)
            : this()
        {
            PokemonId = pokemon.Id;
            PokemonIsStarter = pokemon.IsStarter;
        }
        
        public string PokemonId { get; private set; }

        public bool PokemonIsStarter { get; private set; }
    }
}
