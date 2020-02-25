using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Pokens.Training.Domain
{
    public sealed class PokemonLevel : ValueObject
    {
        private const int ExperienceThreshold = 100;

        private PokemonLevel()
        {
        }

        public static PokemonLevel Default() => new PokemonLevel { Current = 1, Experience = 0 };

        public int Current { get; private set; }

        public int Experience { get; private set; }

        internal PokemonLevel WithMoreExperience(int experience)
        {
            return new PokemonLevel
            {
                Current = this.Current,
                Experience = this.Experience + experience
            };
        }

        internal Result<PokemonLevel> Next()
        {
            return Result.SuccessIf(CanGoToNext, "Not enough experience")
                .Map(() => new PokemonLevel
                {
                    Current = this.Current + 1,
                    Experience = this.Experience - NeededExperience
                });
        }

        internal bool CanGoToNext() => Experience >= NeededExperience;

        private int NeededExperience => Current * ExperienceThreshold;

        public static implicit operator int(PokemonLevel level) => level.Current;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Current;
            yield return Experience;
        }
    }
}