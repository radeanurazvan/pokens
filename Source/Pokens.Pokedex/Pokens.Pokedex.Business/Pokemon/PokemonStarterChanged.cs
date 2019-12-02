using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Pokedex.Business
{
    internal sealed class PokemonStarterChanged : IBusMessage
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
