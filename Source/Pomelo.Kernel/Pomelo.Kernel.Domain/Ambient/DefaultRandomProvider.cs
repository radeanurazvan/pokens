using System;

namespace Pomelo.Kernel.Domain
{
    public class DefaultRandomProvider : IRandomProvider
    {
        private static readonly Random Random;

        static DefaultRandomProvider()
        {
            Random = new Random();
        }

        public double NextDouble() => RandomProviderContext.Current?.RandomValue ?? Random.NextDouble();
    }
}