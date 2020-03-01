using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Pokens.Training.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokens.Training.Domain
{
    public class PokemonDefinition : DocumentAggregateRoot
    {
        private ICollection<Ability> abilities = new List<Ability>();

        private PokemonDefinition()
        {
        }

        private PokemonDefinition(Guid id, string name, Stats stats, double catchRate, IEnumerable<Ability> abilities)
            : this()
        {
            Id = id.ToString();
            Name = name;
            Stats = stats;
            CatchRate = catchRate;
            this.abilities = abilities.ToList();
        }

        public static Result<PokemonDefinition> Create(Guid id, string name, Stats stats, double catchRate, IEnumerable<Ability> abilities)
        {
            var idResult = Result.FailureIf(id == Guid.Empty, Messages.InvalidId);
            var nameResult = name.EnsureValidString(Messages.InvalidName);
            var statsResult = stats.EnsureExists(Messages.NullStats);

            return Result.FirstFailureOrSuccess(idResult, nameResult, statsResult)
                .Map(() => new PokemonDefinition(id, name, stats, catchRate, abilities));
        }

        public string Name { get; private set; }

        public Stats Stats { get; private set; }

        public bool IsStarter { get; private set; }

        public Rate CatchRate { get; private set; }

        public byte[] Image { get; private set; }

        public IEnumerable<Ability> Abilities => this.abilities;

        public void ChangeIsStarter(bool isStarter)
        {
            IsStarter = isStarter;
        }

        public void ChangeImage(byte[] image)
        {
            Image = image;
        }

        public static class Expressions
        {
            public const string Abilities = nameof(abilities);
        }
    }
}