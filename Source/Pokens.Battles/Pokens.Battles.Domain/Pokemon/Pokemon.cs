using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class Pokemon : Entity
    {
        private const int StartingLevel = 1;

        private Pokemon()
        {
        }

        public Pokemon(Guid id, string name, Stats stats)
            : this()
        {
            Id = id;
            Name = name;
            Stats = stats;
            Level = StartingLevel;
        }

        public string Name { get; private set; }

        public int Level { get; private set; }

        public Stats Stats { get; private set; }

        internal void LevelUp() => Level++;
    }
}