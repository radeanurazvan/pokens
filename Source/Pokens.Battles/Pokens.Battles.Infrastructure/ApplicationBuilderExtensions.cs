using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pokens.Battles.Business;
using Pokens.Battles.Domain;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Battles.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDefaultArenas(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<IWriteRepository<Arena>>();
                Constants.DefaultArenas.ForEach(a => repository.Add(a).GetAwaiter().GetResult());
                repository.Save().GetAwaiter().GetResult();
            }

            return app;
        }

        public static IApplicationBuilder UseBattlesBusSubscriptions(this IApplicationBuilder app)
        {
            var bus = app.ApplicationServices.GetService<IMessageBus>();
            bus.Subscribe<IntegrationEvent<TrainerCreatedEvent>>();
            bus.Subscribe<IntegrationEvent<PokemonCaughtEvent>>();
            bus.Subscribe<IntegrationEvent<StarterPokemonChosenEvent>>();
            bus.Subscribe<TrainerAcceptedChallengeEvent>();

            return app;
        }
    }
}