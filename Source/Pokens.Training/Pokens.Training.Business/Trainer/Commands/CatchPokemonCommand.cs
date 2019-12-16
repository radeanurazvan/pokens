using Pomelo.Kernel.Common;

namespace Pokens.Training.Business
{
    public sealed class CatchPokemonCommand : ICommand
    {
        public CatchPokemonCommand(string trainerId, string pokemonId)
        {
            TrainerId = trainerId;
            PokemonId = pokemonId;
        }

        public string TrainerId { get; }

        public string PokemonId { get; }
    }
}
