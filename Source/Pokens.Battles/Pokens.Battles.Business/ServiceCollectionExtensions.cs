using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Battles.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBattlesBusiness(this IServiceCollection services)
        {
            return services.AddBattlesBusHandlers()
                .AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
        }

        private static IServiceCollection AddBattlesBusHandlers(this IServiceCollection services)
        {
            return services.AddScoped<IBusMessageHandler<IntegrationEvent<TrainerCreatedEvent>>, TrainerCreatedEventHandler>()
                .AddScoped<TrainerCreatedEventHandler>()
                .AddScoped<IBusMessageHandler<IntegrationEvent<PokemonCaughtEvent>>, PokemonCaughtEventHandler>()
                .AddScoped<PokemonCaughtEventHandler>()
                .AddScoped<IBusMessageHandler<IntegrationEvent<StarterPokemonChosenEvent>>, StarterPokemonChosenEventHandler>()
                .AddScoped<StarterPokemonChosenEventHandler>();
        }
    }
}