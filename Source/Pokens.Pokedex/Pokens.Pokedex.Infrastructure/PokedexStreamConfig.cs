using System;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.EventStore;

namespace Pokens.Pokedex.Infrastructure
{
    public class PokedexStreamConfig<T> : IStreamConfig<T> where T : IAggregateRoot
    {
        public string GetStreamFor(Guid aggregateId) => $"{nameof(Pokedex)}{typeof(T).GetFriendlyName()}-{aggregateId}";
    }
}