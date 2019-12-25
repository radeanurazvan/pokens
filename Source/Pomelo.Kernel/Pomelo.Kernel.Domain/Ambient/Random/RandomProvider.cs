using System;

namespace Pomelo.Kernel.Domain
{
    public class RandomProvider
    {
        private static readonly Lazy<IRandomProvider> instance = new Lazy<IRandomProvider>(() => new DefaultRandomProvider());

        private RandomProvider()
        {
        }

        public static IRandomProvider Instance()
        {
            return instance.Value;
        }
    }
}