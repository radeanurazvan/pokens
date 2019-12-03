﻿using System;
using CSharpFunctionalExtensions;
using Pokens.Training.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokens.Training.Domain
{
    public class PokemonDefinition : DocumentEntity
    {
        private PokemonDefinition()
        {
        }

        private PokemonDefinition(Guid id, string name, Stats stats)
            : this()
        {
            Id = id.ToString();
            Name = name;
            Stats = stats;
        }

        public static Result<PokemonDefinition> Create(Guid id, string name, Stats stats)
        {
            var idResult = id.EnsureNotEmpty(Messages.InvalidId);
            var nameResult = name.EnsureValidString(Messages.InvalidName);
            var statsResult = stats.EnsureExists(Messages.NullStats);

            return Result.FirstFailureOrSuccess(idResult, nameResult, statsResult)
                .Map(() => new PokemonDefinition(id, name, stats));
        }

        public string Name { get; private set; }

        public Stats Stats { get; private set; }

        public bool IsStarter { get; private set; }

        public void ChangeIsStarter(bool isStarter)
        {
            IsStarter = isStarter;
        }
    }
}