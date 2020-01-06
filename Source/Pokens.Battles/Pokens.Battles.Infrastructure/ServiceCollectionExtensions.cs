using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.EventStore;
using Pomelo.Kernel.Infrastructure;
using Pomelo.Kernel.Messaging;

namespace Pokens.Battles.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBattlesAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddPomeloJwtAuthentication(configuration.GetJwtSettings());
        }

        public static IServiceCollection AddBattlesInfrastructure(this IServiceCollection services)
        {
            return services
                .AddPomeloRabbitMqBus()
                .AddPomeloEventStore()
                .AddPomeloEventSourcedRepositories();
        }
    }
}