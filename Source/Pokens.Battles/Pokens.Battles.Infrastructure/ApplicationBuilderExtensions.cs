using System;
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
        private static readonly Guid NoobGuid = new Guid("C9591069-33E7-45E1-BF71-2166C67397BD");

        public static IApplicationBuilder UseDefaultArenas(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IRepositoryMediator>();
                var existingArena = mediator.ReadById<Arena>(NoobGuid).GetAwaiter().GetResult();
                if (existingArena.HasValue)
                {
                    return app;
                }

                var arenas = Constants.DefaultArenas.ToList();
                typeof(Arena).GetProperty(nameof(Arena.Id)).SetValue(arenas.First(), NoobGuid);

                var writeRepository = mediator.Write<Arena>();
                arenas.ForEach(a => writeRepository.Add(a).GetAwaiter().GetResult());
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