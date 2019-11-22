using Microsoft.Extensions.DependencyInjection;
using Pokens.Trainers.Domain;
using Pomelo.Kernel.Common;

namespace Pokens.Trainers.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddScoped<ITokenService, JwtTokenService>()
                .AddScoped<ICredentialsService, IdentityCredentialsService>()
                .AddSingletonSettings<JwtSettings>();
        }
    }
}