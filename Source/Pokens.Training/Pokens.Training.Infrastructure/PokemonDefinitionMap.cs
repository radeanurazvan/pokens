using MongoDB.Bson.Serialization;
using Pokens.Training.Domain;

namespace Pokens.Training.Infrastructure
{
    internal sealed class PokemonDefinitionMap : BsonClassMap<PokemonDefinition>
    {
        public PokemonDefinitionMap()
        {
            AutoMap();

            UnmapProperty(pd => pd.Abilities);
            MapField(PokemonDefinition.Expressions.Abilities).SetElementName(nameof(PokemonDefinition.Abilities));
        }
    }
}