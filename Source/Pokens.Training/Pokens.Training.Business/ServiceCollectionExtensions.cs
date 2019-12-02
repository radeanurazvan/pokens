using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Training.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTrainingBusiness(this IServiceCollection services)
        {
            return services.AddBusHandlers();
        }

        private static IServiceCollection AddBusHandlers(this IServiceCollection services)
        {
            return services.AddScoped<IBusMessageHandler<IntegrationEvent<TrainerCreatedEvent>>, TrainerCreatedEventHandler>()
                .AddScoped<TrainerCreatedEventHandler>()
                .AddScoped<IBusMessageHandler<PokemonCreated>, PokemonCreatedHandler>()
                .AddScoped<PokemonCreatedHandler>()
                .AddScoped<IBusMessageHandler<PokemonStarterChanged>, PokemonStarterChangedHandler>()
                .AddScoped<PokemonStarterChangedHandler>();
        }
    }
}