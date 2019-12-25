using System;

namespace Pomelo.Kernel.Domain
{
    public class DateTimeProvider 
    {
        private static readonly Lazy<IDateTimeProvider> instance = new Lazy<IDateTimeProvider>(() => new DefaultDateTimeProvider());

        private DateTimeProvider()
        {
        }

        public static IDateTimeProvider Instance()
        {
            return instance.Value;
        }
    }
}