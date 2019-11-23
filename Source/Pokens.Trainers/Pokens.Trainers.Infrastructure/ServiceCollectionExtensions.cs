using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokens.Trainers.Domain;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Infrastructure;

namespace Pokens.Trainers.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTrainersInfrastructure(this IServiceCollection services)
        {
            return services.AddScoped<ITokenService, JwtTokenService>()
                .AddScoped<IUsersService, IdentityUsersService>()
                .AddSingletonSettings<JwtSettings>();
        }

        public static IServiceCollection AddTrainersJwtAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(nameof(JwtSettings))
                .Get<JwtSettings>(o => o.BindNonPublicProperties = true);
            return services
                .AddPomeloJwtAuthentication(jwtSettings);
        }
    }
}