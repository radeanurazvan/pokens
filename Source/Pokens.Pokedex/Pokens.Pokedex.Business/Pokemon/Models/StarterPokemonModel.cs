using System.Linq;
using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Business
{
    public sealed class StarterPokemonModel
    {
        public StarterPokemonModel(Pokemon pokemon)
        {
            Id = pokemon.Id;
            Name = pokemon.Name;
            Image = pokemon.Images.FirstOrDefault()?.ContentImage;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public byte[] Image { get; private set; }
    }
}