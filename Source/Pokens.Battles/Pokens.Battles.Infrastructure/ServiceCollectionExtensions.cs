using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokens.Battles.Business;
using Pomelo.Kernel.Authentication;
using Pomelo.Kernel.EventStore;
using Pomelo.Kernel.EventStore.Repositories;
using Pomelo.Kernel.Http;
using Pomelo.Kernel.Infrastructure;

namespace Pokens.Battles.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBattlesAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddPomeloDefaultJsonSettings(new PrivatePropertyContractResolver())
                .AddPomeloClaimsUser()
                .AddPomeloJwtAuthentication(configuration.GetJwtSettings());
        }

        public static IServiceCollection AddBattlesInfrastructure(this IServiceCollection services)
        {
            return services

                .AddPomeloRepositoryMediator()
                .AddPomeloEventStore()
                .AddConfigurationSettings()
                .AddPomeloTracing()
                .AddEventSourcedRepositories()
                .Services
                .AddPomeloRepositoryMediator()
                .AddBattlesSignalR();
        }

        private static IServiceCollection AddBattlesSignalR(this IServiceCollection services)
        {
            services
                .AddScoped<IBattlesNotifications, BattlesSignalrNotifications>()
                .AddSignalR();
            return services;
        }
    }
}