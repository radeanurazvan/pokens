using System;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.EventStore;

namespace Pokens.Battles.Infrastructure
{
    public sealed class BattlesStreamConfig<T> : IStreamConfig<T> 
        where T : IAggregateRoot
    {
        public string GetStreamFor(Guid aggregateId) => $"{nameof(Battles)}{typeof(T).GetFriendlyName()}-{aggregateId}";
    }
}