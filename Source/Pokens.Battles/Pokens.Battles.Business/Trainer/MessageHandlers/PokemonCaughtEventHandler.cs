using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.Extensions.Logging;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    internal sealed class PokemonCaughtEventHandler : IIntegrationEventHandler<PokemonCaughtEvent>
    {
        private readonly IRepositoryMediator mediator;
        private readonly ILogger logger;

        public PokemonCaughtEventHandler(IRepositoryMediator mediator, ILogger<PokemonCaughtEventHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public Task Handle(IntegrationEvent<PokemonCaughtEvent> message)
        {
            EnsureArg.IsNotNull(message);

            return mediator.ReadById<Trainer>(new Guid(message.Metadata.AggregateId)).ToResult(Messages.TrainerNotFound)
                .Tap(t => t.Catch(GetPokemon(new Guid(message.Metadata.AggregateId), message.Data)))
                .Tap(_ => this.mediator.Write<Trainer>().Save())
                .OnFailure(e => this.logger.LogError(e))
                .OnFailure(e => throw new InvalidOperationException(e));
        }

        private Pokemon GetPokemon(Guid trainerId, PokemonCaughtEvent @event)
        {
            var defensiveStats = new DefensiveStats(@event.Health, @event.Defense, @event.DodgeChance);
            var offensiveStats = new OffensiveStats(@event.AttackPower, @event.CriticalStrikeChance);
            var stats = new Stats(defensiveStats, offensiveStats);
            var abilities = @event.Abilities.Select(a => new Ability(a.Id, a.Name, a.Description, a.Damage, a.RequiredLevel, a.Cooldown));

            return new Pokemon(new Guid(@event.PokemonId), trainerId, @event.DefinitionName, stats, abilities);
        }
    }
}