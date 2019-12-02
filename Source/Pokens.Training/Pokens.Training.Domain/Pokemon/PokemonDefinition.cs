using CSharpFunctionalExtensions;
using Pokens.Training.Resources;
using Pomelo.Kernel.Common;

namespace Pokens.Training.Domain
{
    public class PokemonDefinition
    {
        private PokemonDefinition()
        {
        }

        private PokemonDefinition(string name, Stats stats)
            : this()
        {
            Name = name;
            Stats = stats;
        }

        public static Result<PokemonDefinition> Create(string name, Stats stats)
        {
            var nameResult = name.EnsureValidString(Messages.InvalidName);
            var statsResult = stats.EnsureExists(Messages.NullStats);

            return Result.FirstFailureOrSuccess(nameResult, statsResult)
                .Map(() => new PokemonDefinition(name, stats));
        }

        public string Name { get; private set; }

        public Stats Stats { get; private set; }
    }
}