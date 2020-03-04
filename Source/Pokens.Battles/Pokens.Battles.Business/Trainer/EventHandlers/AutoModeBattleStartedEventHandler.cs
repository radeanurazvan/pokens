using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    internal sealed class AutoModeBattleStartedEventHandler : IDomainEventHandler<BattleStartedEvent>
    {
        private readonly IRepositoryMediator mediator;
        private readonly IBattlesService battlesService;
        private readonly ILogger logger;

        public AutoModeBattleStartedEventHandler(IRepositoryMediator mediator, IBattlesService battlesService, ILogger<AutoModeBattleStartedEventHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.battlesService = battlesService;
        }

        public async Task Handle(BattleStartedEvent @event)
        {
            this.logger.LogError("Auto battle started");
            await mediator.ReadById<Trainer>(@event.AttackerId).ToResult(Messages.TrainerNotFound)
                .Ensure(t => t.IsAutoMode, Messages.TrainerNotInAutoMode)
                .Bind(t => this.battlesService.TakeTurn(@event.Id, t.Id, GetRandomAbility(t, @event.AttackerPokemon.Id)))
                .Tap(() => mediator.Write<Battle>().Save())
                .OnFailure(e => logger.LogWarning($"Failed with message {e}"));
        }

        private Guid GetRandomAbility(Trainer trainer, Guid pokemonId)
        {
            var pokemon = trainer.Pokemons.Single(p => p.Id == pokemonId);
            return pokemon.Abilities.Where(a => a.RequiredLevel <= pokemon.Level)
                .OrderByDescending(a => Guid.NewGuid())
                .First()
                .Id;
        }
    }
}