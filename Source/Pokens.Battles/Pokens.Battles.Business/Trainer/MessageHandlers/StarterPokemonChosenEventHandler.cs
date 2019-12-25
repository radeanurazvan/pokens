using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Battles.Business
{
    internal sealed class StarterPokemonChosenEventHandler : IBusMessageHandler<IntegrationEvent<StarterPokemonChosenEvent>>
    {
        private readonly IRepositoryMediator mediator;

        public StarterPokemonChosenEventHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Handle(IntegrationEvent<StarterPokemonChosenEvent> message)
        {
            EnsureArg.IsNotNull(message);

            return mediator.ReadById<Trainer>(message.Metadata.AggregateId).ToResult(Messages.TrainerNotFound)
                .Tap(t => t.Catch(GetPokemon(message.Data)))
                .Tap(_ => this.mediator.Write<Trainer>().Save());
        }

        private Pokemon GetPokemon(StarterPokemonChosenEvent @event)
        {
            var defensiveStats = new DefensiveStats(@event.Health, @event.Defense, @event.DodgeChance);
            var offensiveStats = new OffensiveStats(@event.AttackPower, @event.CriticalStrikeChance);
            var stats = new Stats(defensiveStats, offensiveStats);
            return new Pokemon(new Guid(@event.PokemonId), @event.DefinitionName, stats);
        }
    }
}