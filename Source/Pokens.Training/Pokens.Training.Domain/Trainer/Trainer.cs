using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Pokens.Training.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using System.Linq;
namespace Pokens.Training.Domain
{
    public sealed class Trainer : DocumentAggregateRoot
    {
        private ICollection<Pokemon> caughtPokemons = new List<Pokemon>();
        private Pokemon starterPokemon;

        private Trainer()
        {
        }

        private Trainer(Guid id, string name)
            : this()
        {
            Id = id.ToString();
            Name = name;
        }

        public static Result<Trainer> Create(Guid id, string name)
        {
            var idResult = Result.FailureIf(id == Guid.Empty, Messages.InvalidId);
            var nameResult = name.EnsureValidString(Messages.InvalidName);

            return Result.FirstFailureOrSuccess(idResult, nameResult)
                .Map(() => new Trainer(id, name));
        }

        public string Name { get; private set; }

        public Maybe<Pokemon> StarterPokemon => this.starterPokemon;

        public IEnumerable<Pokemon> CaughtPokemons => this.caughtPokemons.Concat(StarterPokemon.Select(x => new List<Pokemon>{x}).Unwrap(new List<Pokemon>()));

        public Result ChooseStarter(PokemonDefinition definition)
        {
            return definition.EnsureExists(Messages.InvalidPokemonDefinition)
                .Ensure(d => d.IsStarter, Messages.PokemonNotStarter)
                .Ensure(_ => StarterPokemon.HasNoValue, Messages.TrainerAlreadyHasStarter)
                .Bind(Pokemon.From)
                .Tap(p => this.starterPokemon = p)
                .Tap(p => AddDomainEvent(new StarterPokemonChosenEvent(p.Id, definition)));
        }

        public Result CatchPokemon(PokemonDefinition definition)
        {
            return definition.EnsureExists(Messages.InvalidPokemonDefinition)
                .Ensure(_ => StarterPokemon.HasValue, Messages.TrainerDoesNotHaveStarter)
                .Ensure(d => d.Id != starterPokemon.DefinitionId, Messages.TrainerAlreadyHasThisPokemon)
                .Ensure(d => caughtPokemons.All(p => p.DefinitionId != d.Id), Messages.TrainerAlreadyHasThisPokemon)
                .Ensure(d => d.CatchRate.Test(), Messages.CatchFailed)
                .Bind(Pokemon.From)
                .Tap(p => this.caughtPokemons.Add(p))
                .Tap(p => AddDomainEvent(new PokemonCaughtEvent(p.Id, definition)));
        }

        public Result CollectExperience(string pokemonId, int amount)
        {
            return this.CaughtPokemons.FirstOrNothing(p => p.Id == pokemonId).ToResult(Messages.PokemonNotFound)
                .Tap(p => p.CollectExperience(amount))
                .Tap(p =>
                {
                    while (p.LevelUp().IsSuccess)
                    {
                        AddDomainEvent(new PokemonLeveledUpEvent(p));
                    }
                });
        }

        public static class Expressions
        {
            public const string CaughtPokemons = nameof(caughtPokemons);
            public const string StarterPokemon = nameof(starterPokemon);
        }
    }
}