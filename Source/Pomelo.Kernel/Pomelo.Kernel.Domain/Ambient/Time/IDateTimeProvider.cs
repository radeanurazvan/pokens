using System;

namespace Pomelo.Kernel.Domain
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}