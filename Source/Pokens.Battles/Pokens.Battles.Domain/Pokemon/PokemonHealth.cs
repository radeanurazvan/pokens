using System;

namespace Pokens.Battles.Domain
{
    internal static class PokemonHealth
    {
        private const int Multiplier = 10;
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public static int GetBonusHealth(int newLevel)
        {
            var jitter = Random.Next(-5, 10);
            var lowerBound = (newLevel - 1) * Multiplier + jitter;
            var upperBound = newLevel * Multiplier;

            return Random.Next(lowerBound, upperBound);
        }
    }
}