using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.EventStore;
using Pomelo.Kernel.Mongo;

namespace Pokens.Pokedex.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPokedexInfrastructure(this IServiceCollection services)
        {
            return services
                .AddPomeloAggregatesContext()
                .AddPomeloEventStore()
                .AddPomeloMongoCollectionRepository();
        }
    }
}