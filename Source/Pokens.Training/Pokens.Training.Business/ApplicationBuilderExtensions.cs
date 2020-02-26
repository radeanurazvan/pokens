using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Business
{
    public static class ApplicationBuilderExtensions
    {
        private const string TrainersTag = "Trainers";
        private const string PokedexTag = "Pokedex";
        private const string BattlesTag = "Battles";

        public static IApplicationBuilder UseTrainingSubscriptions(this IApplicationBuilder app)
        {
            Task.Delay(TimeSpan.FromSeconds(5)).GetAwaiter().GetResult();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var subscriptions = scope.ServiceProvider.GetService<IEventSubscriptions>();

                subscriptions.SubscribeIntegrationEvent<TrainerCreatedEvent>(TrainersTag).GetAwaiter().GetResult();
                subscriptions.SubscribeIntegrationEvent<PokemonCreated>(PokedexTag).GetAwaiter().GetResult();
                subscriptions.SubscribeIntegrationEvent<PokemonStarterChanged>(PokedexTag).GetAwaiter().GetResult();
                subscriptions.SubscribeIntegrationEvent<PokemonImagesChanged>(PokedexTag).GetAwaiter().GetResult();
                subscriptions.SubscribeIntegrationEvent<TrainerCollectedExperienceEvent>(BattlesTag).GetAwaiter().GetResult();
            }
            return app;
        }
    }
}