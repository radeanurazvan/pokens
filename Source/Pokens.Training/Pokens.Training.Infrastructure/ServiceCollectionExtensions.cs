using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using Pomelo.Kernel.EventStore;
using Pomelo.Kernel.Infrastructure;
using Pomelo.Kernel.Messaging;
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
                .AddPomeloMongoCollectionRepository()
                .AddPomeloRabbitMqBus()
                .AddMongoMaps();
        }

        private static IServiceCollection AddMongoMaps(this IServiceCollection services)
        {
            BsonClassMap.RegisterClassMap(new AggregateMap());
            BsonClassMap.RegisterClassMap(new TrainerMap());
            return services;
        }
    }
}