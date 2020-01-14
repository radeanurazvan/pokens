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
        {
            Name = definition.Name;
            DefinitionId = definition.Id;
            Image = definition.Image;
        }

        public static Result<Pokemon> From(PokemonDefinition definition)
        {
            return definition.EnsureExists(Messages.InvalidPokemonDefinition)
                .Map(d => new Pokemon(d));
        }

        public string DefinitionId { get; private set; }

        public string Name { get; private set; }

        public byte[] Image { get; set; }
    }
}