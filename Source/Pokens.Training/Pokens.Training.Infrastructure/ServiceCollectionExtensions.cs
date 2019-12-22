using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using Pomelo.Kernel.Messaging;
using Pomelo.Kernel.Mongo;

namespace Pokens.Training.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTrainingInfrastructure(this IServiceCollection services)
        {
            return services
                .AddPomeloMongoCollectionRepository()
                .AddPomeloRabbitMqBus()
                .AddMongoMaps();
        }

        private static IServiceCollection AddMongoMaps(this IServiceCollection services)
        {
            BsonClassMap.RegisterClassMap<TrainerMap>();
            BsonClassMap.RegisterClassMap<PokemonDefinitionMap>();
            return services;
        }
    }
}