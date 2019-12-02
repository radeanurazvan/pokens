using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Pokens.Training.Domain
{
    public sealed class Stats : ValueObject
    {
        public Stats(int health, int defense, int attackPower, float criticalStrikeChance, float dodgeChance)
        {
            Health = health;
            Defense = defense;
            AttackPower = attackPower;
            CriticalStrikeChance = criticalStrikeChance;
            DodgeChance = dodgeChance;
        }

        public int Health { get; private set; }

        public int Defense { get; private set; }

        public int AttackPower { get; private set; }

        public float CriticalStrikeChance { get; private set; }

        public float DodgeChance { get; private set; }

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