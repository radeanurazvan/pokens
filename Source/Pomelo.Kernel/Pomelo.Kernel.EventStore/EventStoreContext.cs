using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    internal sealed class EventStoreContext
    {
        private readonly ICollection<AggregateRoot> attachedAggregates = new List<AggregateRoot>();
        public IEnumerable<AggregateRoot> AttachedAggregates => attachedAggregates;

        public void Attach(AggregateRoot aggregate)
        {
            attachedAggregates.FirstOrNothing(a => a == aggregate)
                .ToResult("No duplicate")
                .OnFailure(() => attachedAggregates.Add(aggregate));
        }

        public Result<T> GetById<T>(Guid id) 
            where T : AggregateRoot
        {
            return this.attachedAggregates.FirstOrNothing(a => a.Id == id)
                .ToResult("No aggregate attached")
                .Map(a => a as T);
        }
    }
}