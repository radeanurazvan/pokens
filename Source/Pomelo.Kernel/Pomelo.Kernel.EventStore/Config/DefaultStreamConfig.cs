using System;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    internal sealed class DefaultStreamConfig<T> : IStreamConfig<T>
        where T : IAggregateRoot
    {
        public string GetStreamFor(Guid aggregateId)
        {
            return $"{typeof(T).FullName}-{aggregateId}";
        }

        public string GetCategoryStream()
        {
            return $"$ce-{typeof(T).FullName}";
        }
    }
}