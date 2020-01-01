using System;

namespace Pokens.Training.Domain.Tests
{
    public static class PokemonDefinitionFactory
    {
        public static PokemonDefinition Starter() => Starter("Starter");

        public static PokemonDefinition Starter(string name)
        {
            var definition = PokemonDefinition.Create(Guid.NewGuid(), name, StatsFactory.Empty(), 100).Value;
            definition.ChangeIsStarter(true);

            return definition;
        }

        public static PokemonDefinition GetPokemonDefinition(string name)
        {
            return PokemonDefinition.Create(Guid.NewGuid(), name, StatsFactory.Empty(), 100).Value;
        }

        public static PokemonDefinition GetPokemonDefinition(string name, double catchRate)
        {
            return PokemonDefinition.Create(Guid.NewGuid(), name, StatsFactory.Empty(), catchRate).Value;
        }

        public static PokemonDefinition NotStarter() => PokemonDefinition.Create(Guid.NewGuid(), "Not starter", StatsFactory.Empty(), 100).Value;
    }
}