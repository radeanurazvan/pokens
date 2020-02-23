using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.EventStore;
using Pomelo.Kernel.Http;
using Pomelo.Kernel.Mongo;

namespace Pokens.Training.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTrainingInfrastructure(this IServiceCollection services)
        {
            return services
                .AddPomeloClaimsUser()
                .AddPomeloEventStore()
                .AddPomeloEventStoreSubscriptions()
                .AddPomeloAggregatesContext()
                .AddPomeloMongoCollectionRepository()
                .AddMongoMaps();
        }

        private static IServiceCollection AddMongoMaps(this IServiceCollection services)
        {
            BsonClassMap.RegisterClassMap(new AggregateMap());
            BsonClassMap.RegisterClassMap(new TrainerMap());
            BsonClassMap.RegisterClassMap(new PokemonDefinitionMap());
            return services;
        }
    }
}