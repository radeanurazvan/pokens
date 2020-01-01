using System;
using System.Collections.Generic;

namespace Pomelo.Kernel.Domain
{
    public interface IAggregateRoot
    {
        Guid GetId();

        IReadOnlyList<IDomainEvent> Events { get; }
    }
}