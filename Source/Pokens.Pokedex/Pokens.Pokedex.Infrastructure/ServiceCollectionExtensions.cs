using Microsoft.Extensions.DependencyInjection;
using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Common;

namespace Pokens.Pokedex.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPokedexInfrastructure(this IServiceCollection services)
        {
            return services.AddScoped<IPokedexRepository, PokedexMongoRepository>()
                .AddSingletonSettings<PokedexMongoSettings>();
        }
    }
}