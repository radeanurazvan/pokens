using Microsoft.Extensions.DependencyInjection;

namespace Pokens.Pokedex.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPokedexServices(this IServiceCollection services)
        {
            return services.AddScoped<IPokemonService, PokemonService>()
                .AddScoped<IAbilityService, AbilityService>();
        }
    }
}