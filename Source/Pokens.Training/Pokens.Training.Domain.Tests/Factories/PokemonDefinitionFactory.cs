using System;

namespace Pokens.Training.Domain.Tests
{
    public static class PokemonDefinitionFactory
    {
        public static PokemonDefinition Starter() => Starter("Starter");

        public static PokemonDefinition Starter(string name)
        {
            var definition = PokemonDefinition.Create(Guid.NewGuid(), name, StatsFactory.Empty()).Value;
            definition.ChangeIsStarter(true);

            return definition;
        }

        public static PokemonDefinition NotStarter() => PokemonDefinition.Create(Guid.NewGuid(), "Not starter", StatsFactory.Empty()).Value;
    }
}