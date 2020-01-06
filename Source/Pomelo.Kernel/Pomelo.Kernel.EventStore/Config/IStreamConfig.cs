using System;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    public interface IStreamConfig<T>
        where T : AggregateRoot
    {
        string GetStreamFor(Guid aggregateId);
        
        string GetCategoryStream();
    }
}