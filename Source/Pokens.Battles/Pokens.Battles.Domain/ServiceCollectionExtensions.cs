using Microsoft.Extensions.DependencyInjection;

namespace Pokens.Battles.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBattlesDomain(this IServiceCollection services)
        {
            return services.AddScoped<IBattlesService, BattleService>();
        }
    }
}