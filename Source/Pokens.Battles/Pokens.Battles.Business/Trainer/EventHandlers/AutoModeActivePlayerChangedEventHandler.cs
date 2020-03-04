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
    internal sealed class AutoModeActivePlayerChangedEventHandler : IDomainEventHandler<ActivePlayerChangedEvent>
    {
        private const int Retries = 150;

        private readonly IRepositoryMediator mediator;
        private readonly IBattlesService battlesService;
        private readonly ILogger logger;

        public AutoModeActivePlayerChangedEventHandler(IRepositoryMediator mediator, IBattlesService battlesService, ILogger<AutoModeActivePlayerChangedEventHandler> logger)
        {
            this.mediator = mediator;
            this.battlesService = battlesService;
            this.logger = logger;
        }

        public async Task Handle(ActivePlayerChangedEvent @event)
        {
            this.logger.LogError($"Received active player changed event. Active player: {@event.ActivePlayer}, Battle: {@event.BattleId}");
            await mediator.ReadById<Trainer>(@event.ActivePlayer).ToResult(Messages.TrainerNotFound)
                .Ensure(t => t.IsAutoMode, Messages.TrainerNotInAutoMode)
                .Bind(t => TakeTurn(t, @event))
                .Tap(() => mediator.Write<Battle>().Save())
                .OnFailure(e => logger.LogWarning($"Failed with message {e}"));
        }

        private async Task<Result> TakeTurn(Trainer trainer, ActivePlayerChangedEvent @event)
        {
            var retry = 0;
            while (retry < Retries)
            {
                var ability = GetRandomAbility(trainer, @event.ActivePokemon);
                var result = await battlesService.TakeTurn(@event.BattleId, trainer.Id, ability);
                retry++;

                if (result.IsFailure && result.Error != Messages.AbilityIsOnCooldown)
                {
                    return result;
                }

                if (result.IsSuccess)
                {
                    return result;
                }
            }

            return Result.Failure($"Could not use ability after {Retries} retries");
        }

        private Guid GetRandomAbility(Trainer trainer, Guid pokemonId)
        {
            var pokemon = trainer.Pokemons.Single(p => p.Id == pokemonId);
            return pokemon.Abilities.Where(a => a.RequiredLevel <= pokemon.Level)
                .OrderBy(a => Guid.NewGuid())
                .First()
                .Id;
        }
    }
}