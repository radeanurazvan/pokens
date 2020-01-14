using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Training.Business
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseTrainingBusSubscriptions(this IApplicationBuilder app)
        {
            var bus = app.ApplicationServices.GetService<IMessageBus>();
            bus.Subscribe<IntegrationEvent<TrainerCreatedEvent>>();
            bus.Subscribe<PokemonCreated>();
            bus.Subscribe<PokemonStarterChanged>();
            bus.Subscribe<PokemonImagesChanged>();

            return app;
        }
    }
}