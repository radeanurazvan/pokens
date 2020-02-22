using MongoDB.Bson.Serialization;
using Pomelo.Kernel.Domain;

namespace Pokens.Pokedex.Infrastructure.Maps
{
    internal sealed class AggregateRootMap : BsonClassMap<DocumentAggregateRoot>
    {
        public AggregateRootMap()
        {
            UnmapProperty(a => a.Events);
            UnmapField("events");
        }   
    }
}