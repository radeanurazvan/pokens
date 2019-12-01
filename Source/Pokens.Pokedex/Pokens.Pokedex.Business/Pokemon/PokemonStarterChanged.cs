using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Messaging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokens.Pokedex.Business
{
    internal sealed class PokemonStarterChanged : IBusMessage
    {
        private PokemonStarterChanged()
        {

        }

        public PokemonStarterChanged(Pokemon pokemon)
        {
            PokemonId = pokemon.Id;
            PokemonIsStarter = pokemon.IsStarter;
        }
        public string PokemonId { get; private set; }

        public bool PokemonIsStarter { get; private set; }
    }
}
