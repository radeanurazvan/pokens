using System;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    internal sealed class DefaultStreamConfig<T> : IStreamConfig<T>
        where T : AggregateRoot
    {
        public string GetStreamFor(Guid aggregateId)
        {
            return $"{typeof(T).Name}-{aggregateId}";
        }

        public string GetCategoryStream()
        {
            return $"$ce-{typeof(T).Name}";
        }
    }
}