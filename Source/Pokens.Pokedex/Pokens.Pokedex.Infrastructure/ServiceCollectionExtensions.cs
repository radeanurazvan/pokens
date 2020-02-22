using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using Pokens.Pokedex.Infrastructure.Maps;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.EventStore;
using Pomelo.Kernel.Mongo;

namespace Pokens.Pokedex.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPokedexInfrastructure(this IServiceCollection services)
        {
            BsonClassMap.RegisterClassMap(new AggregateRootMap());
            return services
                .AddPomeloAggregatesContext()
                .AddPomeloEventStore()
                .AddPomeloMongoCollectionRepository()
                .AddSingleton(typeof(IStreamConfig<>), typeof(PokedexStreamConfig<>));
        }
    }
}