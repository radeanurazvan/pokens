using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pokens.Battles.Business;
using Pokens.Battles.Domain;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDefaultArenas(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var writeRepository = scope.ServiceProvider.GetService<IWriteRepository<Arena>>();
                Constants.DefaultArenas.ToList().ForEach(a => writeRepository.Add(a).GetAwaiter().GetResult());
                writeRepository.Save().GetAwaiter().GetResult();
            }

            return app;
        }

        public static IApplicationBuilder UseBattlesSubscriptions(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var subscriptions = scope.ServiceProvider.GetService<IEventSubscriptions>();
                subscriptions.SubscribeIntegrationEvent<TrainerCreatedEvent>("Trainers");
                subscriptions.SubscribeIntegrationEvent<PokemonCaughtEvent>("Training");
                subscriptions.SubscribeIntegrationEvent<StarterPokemonChosenEvent>("Training");
                subscriptions.SubscribeDomainEvent<TrainerAcceptedChallengeEvent>("Battles");
            }

            return app;
        }
    }
}