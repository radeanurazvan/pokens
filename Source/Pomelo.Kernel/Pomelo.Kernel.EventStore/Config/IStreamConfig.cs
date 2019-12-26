using System;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    public interface IStreamConfig<T>
        where T : IAggregateRoot
    {
        string GetStreamFor(Guid aggregateId);
    }
}