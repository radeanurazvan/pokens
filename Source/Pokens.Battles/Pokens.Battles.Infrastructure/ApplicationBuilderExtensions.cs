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
                var mediator = scope.ServiceProvider.GetService<IRepositoryMediator>();
                if (mediator.Read<Arena>().GetAll().GetAwaiter().GetResult().Any())
                {
                    return app;
                }

                var writeRepository = mediator.Write<Arena>();
                Constants.DefaultArenas.ToList().ForEach(a => writeRepository.Add(a).GetAwaiter().GetResult());
                writeRepository.Save().GetAwaiter().GetResult();
            }

            return app;
        }

        public static IApplicationBuilder UseBattlesBusSubscriptions(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var subscriptions = scope.ServiceProvider.GetService<IEventSubscriptions>();
                subscriptions.SubscribeIntegrationEvent<TrainerCreatedEvent>("Trainers");
                subscriptions.SubscribeIntegrationEvent<PokemonCaughtEvent>();
                subscriptions.SubscribeIntegrationEvent<StarterPokemonChosenEvent>();
                subscriptions.SubscribeDomainEvent<TrainerAcceptedChallengeEvent>();
            }

            return app;
        }
    }
}