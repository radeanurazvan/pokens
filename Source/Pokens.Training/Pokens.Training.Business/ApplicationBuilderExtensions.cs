using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Business
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseTrainingBusSubscriptions(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var subscriptions = scope.ServiceProvider.GetService<IEventSubscriptions>();

                subscriptions.SubscribeIntegrationEvent<TrainerCreatedEvent>();
                subscriptions.SubscribeIntegrationEvent<PokemonCreated>();
                subscriptions.SubscribeIntegrationEvent<PokemonStarterChanged>();
                subscriptions.SubscribeIntegrationEvent<PokemonImagesChanged>();
            }
            return app;
        }
    }
}