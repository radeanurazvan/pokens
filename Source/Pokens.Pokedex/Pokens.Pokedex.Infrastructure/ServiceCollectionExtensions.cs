using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.EventStore;
using Pomelo.Kernel.Messaging;
using Pomelo.Kernel.Mongo;

namespace Pokens.Pokedex.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPokedexInfrastructure(this IServiceCollection services)
        {
            return services
                .AddPomeloEventStore()
                .AddPomeloMongoCollectionRepository()
                .AddPomeloRabbitMqBus();
        }
    }
}