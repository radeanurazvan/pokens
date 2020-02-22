using System;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.EventStore;

namespace Pokens.Trainers.Infrastructure
{
    public sealed class TrainersStreamConfig<T> : IStreamConfig<T> where T : IAggregateRoot
    {
        public string GetStreamFor(Guid aggregateId) => $"{nameof(Trainers)}{typeof(T).GetFriendlyName()}-{aggregateId}";
    }
}