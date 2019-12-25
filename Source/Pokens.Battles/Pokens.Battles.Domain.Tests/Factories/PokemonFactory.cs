using System;
using System.Linq;
using Pomelo.Kernel.Common;

namespace Pokens.Battles.Domain.Tests
{
    public static class PokemonFactory
    {
        public static Pokemon Pikachu() => Pikachu(0);

        public static Pokemon Pikachu(int level)
        {
            var stats = new Stats(new DefensiveStats(0, 0, 0), new OffensiveStats(0, 0));
            var pokemon = new Pokemon(Guid.NewGuid(), "Pikachu", stats);
            Enumerable.Range(0, level-1).ForEach(_ => pokemon.LevelUp());

            return pokemon;
        }
    }
}