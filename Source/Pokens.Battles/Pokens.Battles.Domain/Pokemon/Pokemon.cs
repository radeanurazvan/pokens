using System;
using System.Collections.Generic;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class Pokemon : Entity
    {
        private readonly ICollection<Ability> abilities = new List<Ability>();
        private const int StartingLevel = 1;

        private Pokemon()
        {
        }

        public Pokemon(Guid id, Guid trainerId, string name, Stats stats, IEnumerable<Ability> abilities)
            : this()
        {
            Id = id;
            TrainerId = trainerId;
            Name = name;
            Stats = stats;
            Level = StartingLevel;
            this.abilities.AddRange(abilities);
        }

        public Guid TrainerId { get; private set; }

        public string Name { get; private set; }

        public int Level { get; private set; }

        public Stats Stats { get; private set; }

        public IEnumerable<Ability> Abilities => this.abilities;

        internal void LevelUp() => Level++;
    }
}