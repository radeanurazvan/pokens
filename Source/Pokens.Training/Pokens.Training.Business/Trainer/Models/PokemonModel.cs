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
            Level = pokemon.Level;
        }

        public string Id { get; }

        public string TrainerId { get; }

        public string Name { get; }

        public int Level { get; private set; }

        public byte[] Image { get; set; }
    }
}