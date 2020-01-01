using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Pokens.Battles.Domain
{
    public sealed class OffensiveStats : ValueObject
    {
        public OffensiveStats(int attackPower, float criticalStrikeChance)
        {
            AttackPower = attackPower;
            CriticalStrikeChance = criticalStrikeChance;
        }

        public int AttackPower { get; private set; }

        public float CriticalStrikeChance { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AttackPower;
            yield return CriticalStrikeChance;
        }
    }
}