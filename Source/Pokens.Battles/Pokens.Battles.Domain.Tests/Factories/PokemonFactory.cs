using System;
using System.Collections.Generic;
using System.Linq;
using Pomelo.Kernel.Common;

namespace Pokens.Battles.Domain.Tests
{
    public static class PokemonFactory
    {
        public static Pokemon Pikachu(int level, Guid trainerId)
        {
            var stats = new Stats(new DefensiveStats(0, 0, 0), new OffensiveStats(0, 0));
            var pokemon = new Pokemon(Guid.NewGuid(), trainerId, "Pikachu", stats, new List<Ability>());
            Enumerable.Range(0, level-1).ForEach(_ => pokemon.LevelUp());

            return pokemon;
        }
    }
}