using System;

namespace Pokens.Pokedex.Business
{
    public static class PokemonRoulette
    {
        private const int BelowFiveRatio = 0;
        private const int BetweenFiveAndTenRatio = 70;
        private const int AboveTenRatio = 90;

        public const int MaxRouletteResult = 12;

        static PokemonRoulette()
        {
            Random = new Random();
        }

        public static Random Random;


        public static int SpinRoulette()
        {
            var luckRatio = Random.Next(0, 100);

            if (luckRatio >= AboveTenRatio)
            {
                return Random.Next(10, MaxRouletteResult);
            }

            if (luckRatio >= BetweenFiveAndTenRatio)
            {
                return Random.Next(5, 10);
            }

            return Random.Next(0, 5);
        }
    }
}