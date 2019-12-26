﻿using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Pokens.Battles.Domain
{
    public sealed class DefensiveStats : ValueObject
    {
        public DefensiveStats(int health, int defense, float dodgeChance)
        {
            Health = health;
            Defense = defense;
            DodgeChance = dodgeChance;
        }

        public int Health { get; private set; }

        public int Defense { get; private set; }

        public float DodgeChance { get; private set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Health;
            yield return Defense;
            yield return DodgeChance;
        }
    }
}