using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using Pokens.Pokedex.Infrastructure.Maps;
using Pomelo.Kernel.EventStore;
using Pomelo.Kernel.Mongo;

namespace Pokens.Pokedex.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPokedexInfrastructure(this IServiceCollection services, string appName)
        {
            BsonClassMap.RegisterClassMap(new AggregateRootMap());
            return services
                .AddPomeloEventStore(appName)
                .AddConfigurationSettings()
                .AddPomeloTracing()
                .Services
                .AddPomeloMongoCollectionRepository();
        }
    }
}