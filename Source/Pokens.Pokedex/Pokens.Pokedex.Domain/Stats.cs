using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Pokens.Pokedex.Domain
{
    public sealed class Stats : ValueObject
    {
        public int Health { get; set; }
        
        public int Defense { get; set; }

        public int AttackPower { get; set; }
        
        public float CriticalStrikeChance { get; set; }

        public float DodgeChance { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Health;
            yield return AttackPower;
            yield return Defense;
            yield return DodgeChance;
            yield return CriticalStrikeChance;
        }
    }
}