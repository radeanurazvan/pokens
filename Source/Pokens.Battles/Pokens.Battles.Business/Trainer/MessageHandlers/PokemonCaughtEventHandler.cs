using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    internal sealed class PokemonCaughtEventHandler : IIntegrationEventHandler<PokemonCaughtEvent>
    {
        private readonly IRepositoryMediator mediator;

        public PokemonCaughtEventHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Handle(IntegrationEvent<PokemonCaughtEvent> message)
        {
            EnsureArg.IsNotNull(message);

            return mediator.ReadById<Trainer>(message.Metadata.AggregateId).ToResult(Messages.TrainerNotFound)
                .Tap(t => t.Catch(GetPokemon(message.Data)))
                .Tap(_ => this.mediator.Write<Trainer>().Save());
        }

        private Pokemon GetPokemon(PokemonCaughtEvent @event)
        {
            var defensiveStats = new DefensiveStats(@event.Health, @event.Defense, @event.DodgeChance);
            var offensiveStats = new OffensiveStats(@event.AttackPower, @event.CriticalStrikeChance);
            var stats = new Stats(defensiveStats, offensiveStats);
            var abilities = @event.Abilities.Select(a => new Ability(a.Id, a.Name, a.Description, a.Damage, a.RequiredLevel, a.Cooldown));

            return new Pokemon(new Guid(@event.PokemonId), @event.DefinitionName, stats, abilities);
        }
    }
}