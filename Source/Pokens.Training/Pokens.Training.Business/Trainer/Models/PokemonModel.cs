using Pokens.Training.Domain;

namespace Pokens.Training.Business
{
    public sealed class PokemonModel
    {
        public PokemonModel(string trainerId, Pokemon pokemon)
        {
            Id = pokemon.Id;
            TrainerId = trainerId;
            Name = pokemon.Name;
            Image = pokemon.Image;
        }

        public string Id { get; }

        public string TrainerId { get; }

        public string Name { get; }

        public byte[] Image { get; set; }
    }
}