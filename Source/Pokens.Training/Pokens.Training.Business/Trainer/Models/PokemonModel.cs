using Pokens.Training.Domain;

namespace Pokens.Training.Business
{
    public sealed class PokemonModel
    {
        public PokemonModel(Pokemon pokemon)
        {
            Id = pokemon.Id;
            Name = pokemon.Name;
            Image = pokemon.Image;
        }

        public string Id { get; }

        public string Name { get; }

        public byte[] Image { get; set; }
    }
}