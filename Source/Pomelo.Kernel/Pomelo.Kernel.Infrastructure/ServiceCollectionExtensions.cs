using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultJsonSettings(this IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateCamelcaseResolver()
            };
            return services;
        }

        public static IServiceCollection AddJsonAppSettings(this IServiceCollection services)
        {
            return services.AddSingleton<IConfiguration>(_ => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
        }

        public static IServiceCollection AddPomeloCors(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("ClientOrigins").Get<string[]>();
            return services.AddCors(o =>
            {
                var policy = new CorsPolicyBuilder(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .Build();
                o.AddPolicy("PomeloCors", policy);
            });
        }

        public static IServiceCollection AddRepositoryMediator(this IServiceCollection services)
        {
            return services.AddScoped<IRepositoryMediator, RepositoryMediator>();
        }
    }
}