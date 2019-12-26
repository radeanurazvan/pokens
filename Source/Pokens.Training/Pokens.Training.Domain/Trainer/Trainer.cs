﻿using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Pokens.Training.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using System.Linq;
namespace Pokens.Training.Domain
{
    public sealed class Trainer : DocumentAggregate
    {
        private readonly ICollection<Pokemon> caughtPokemons = new List<Pokemon>();
        private Pokemon _starterPokemon;

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
            var idResult = id.EnsureNotEmpty(Messages.InvalidId);
            var nameResult = name.EnsureValidString(Messages.InvalidName);

            return Result.FirstFailureOrSuccess(idResult, nameResult)
                .Map(() => new Trainer(id, name));
        }

        public string Name { get; private set; }

        public Maybe<Pokemon> StarterPokemon => this._starterPokemon;

        public IEnumerable<Pokemon> CaughtPokemons => this.caughtPokemons;

        public Result ChooseStarter(PokemonDefinition definition)
        {
            return definition.EnsureExists(Messages.InvalidPokemonDefinition)
                .Ensure(d => d.IsStarter, Messages.PokemonNotStarter)
                .Ensure(_ => StarterPokemon.HasNoValue, Messages.TrainerAlreadyHasStarter)
                .Bind(Pokemon.From)
                .Tap(p => this._starterPokemon = p)
                .Tap(p => AddDomainEvent(new StarterPokemonChosenEvent(p.Id, definition)));
        }

        public Result CatchPokemon(PokemonDefinition definition)
        {
            return definition.EnsureExists(Messages.InvalidPokemonDefinition)
                .Ensure(d => d.CatchRate.Test(), Messages.CatchFailed)
                .Ensure(d => caughtPokemons.All(p => p.DefinitionId != d.Id), Messages.TrainerAlreadyHasThisPokemon)
                .Bind(Pokemon.From)
                .Tap(p => this.caughtPokemons.Add(p))
                .Tap(p => AddDomainEvent(new PokemonCaughtEvent(p.Id, definition)));
        }

        public static class Expressions
        {
            public const string CaughtPokemons = nameof(caughtPokemons);
            public const string StarterPokemon = nameof(_starterPokemon);
        }
    }
}