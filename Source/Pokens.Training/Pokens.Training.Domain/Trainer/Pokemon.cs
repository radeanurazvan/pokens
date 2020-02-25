using CSharpFunctionalExtensions;
using Pokens.Training.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokens.Training.Domain
{
    public sealed class Pokemon : DocumentEntity
    {
        private Pokemon()
        {
        }

        private Pokemon(PokemonDefinition definition)
             : this()
        {
            Name = definition.Name;
            DefinitionId = definition.Id;
            Image = definition.Image;
            Level = PokemonLevel.Default();
        }

        public static Result<Pokemon> From(PokemonDefinition definition)
        {
            return definition.EnsureExists(Messages.InvalidPokemonDefinition)
                .Map(d => new Pokemon(d));
        }

        public string DefinitionId { get; private set; }

        public string Name { get; private set; }

        public PokemonLevel Level { get; private set; }

        public byte[] Image { get; set; }

        internal void CollectExperience(int points)
        {
            this.Level = this.Level.WithMoreExperience(points);
        }

        internal Result LevelUp()
        {
            return this.Level.Next()
                .Tap(l => this.Level = l);
        }
    }
}