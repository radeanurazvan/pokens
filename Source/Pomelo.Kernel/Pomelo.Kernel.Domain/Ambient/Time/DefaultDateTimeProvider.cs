using System;

namespace Pomelo.Kernel.Domain
{
    internal sealed class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTimeProviderContext.Current?.UtcNow ?? DateTime.UtcNow;
    }
}