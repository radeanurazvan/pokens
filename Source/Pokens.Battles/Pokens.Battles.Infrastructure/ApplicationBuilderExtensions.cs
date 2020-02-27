using System;
using System.Linq;
using System.Threading.Tasks;
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
        private const string TrainersTag = "Trainers";
        private const string TrainingTag = "Training";
        private const string BattlesTag = "Battles";

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
                typeof(ArenaOpenedEvent).GetProperty(nameof(ArenaOpenedEvent.Id)).SetValue(arenas.First().Events.First(), NoobGuid);

                var writeRepository = mediator.Write<Arena>();
                arenas.ForEach(a => writeRepository.Add(a).GetAwaiter().GetResult());
                writeRepository.Save().GetAwaiter().GetResult();
            }

            return app;
        }

        public static IApplicationBuilder UseBattlesSubscriptions(this IApplicationBuilder app)
        {
            Task.Delay(TimeSpan.FromSeconds(5)).GetAwaiter().GetResult();
            
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var subscriptions = scope.ServiceProvider.GetService<IEventSubscriptions>();
                subscriptions.SubscribeIntegrationEvent<TrainerCreatedEvent>(TrainersTag).GetAwaiter().GetResult();
                subscriptions.SubscribeIntegrationEvent<PokemonCaughtEvent>(TrainingTag).GetAwaiter().GetResult();
                subscriptions.SubscribeIntegrationEvent<StarterPokemonChosenEvent>(TrainingTag).GetAwaiter().GetResult();
                subscriptions.SubscribeIntegrationEvent<PokemonLeveledUpEvent>(TrainingTag).GetAwaiter().GetResult();
                subscriptions.SubscribeDomainEvent<TrainerAcceptedChallengeEvent>(BattlesTag).GetAwaiter().GetResult();
                subscriptions.SubscribeDomainEvent<TrainerStartedBattleEvent>(BattlesTag).GetAwaiter().GetResult();
                subscriptions.SubscribeDomainEvent<BattleEndedEvent>(BattlesTag).GetAwaiter().GetResult();
            }

            return app;
        }
    }
}