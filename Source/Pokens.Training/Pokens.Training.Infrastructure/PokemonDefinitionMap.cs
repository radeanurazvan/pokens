using MongoDB.Bson.Serialization;
using Pokens.Training.Domain;

namespace Pokens.Training.Infrastructure
{
    internal class PokemonDefinitionMap : BsonClassMap<PokemonDefinition>
    {
        public PokemonDefinitionMap()
        {
            AutoMap();
            UnmapProperty(t => t.Events);
        }
    }
}