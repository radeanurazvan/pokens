using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Messaging.Abstractions;
using System.Collections.Generic;

namespace Pokens.Pokedex.Business
{
    internal sealed class PokemonImagesChanged : IBusMessage
    {
        private Pokemon pokemon;
        private PokemonImagesChanged()
        {
            PokemonId = pokemon.Id;
            Images = pokemon.Images;
        }

        public PokemonImagesChanged(Pokemon pokemon)
            : this()
        {
            this.pokemon = pokemon;
        }

        public string PokemonId { get; private set; }

        public ICollection<Image> Images{ get; private set; }
    }
}