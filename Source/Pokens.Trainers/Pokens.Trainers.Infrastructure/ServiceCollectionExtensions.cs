using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokens.Trainers.Domain;
using Pomelo.Kernel.Authentication;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.EntityFramework;
using Pomelo.Kernel.EventStore;

namespace Pokens.Trainers.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTrainersInfrastructure(this IServiceCollection services)
        {
            return services.AddScoped<ITokenService, JwtTokenService>()
                .AddScoped<IUsersService, IdentityUsersService>()
                .AddPomeloSingletonSettings<JwtSettings>()
                .AddPomeloEntityFrameworkRepositories()
                .AddPomeloEventStore("Trainers.Api")
                .AddConfigurationSettings()
                .AddPomeloTracing()
                .Services;
        }

        public static IServiceCollection AddTrainersJwtAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddPomeloJwtAuthentication(configuration.GetJwtSettings());
        }
    }
}