using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.Mongo
{
    internal sealed class MongoContext 
    {
        private readonly ICollection<IAggregateRoot> attachedAggregates = new List<IAggregateRoot>();
        public IEnumerable<IAggregateRoot> AttachedAggregates => attachedAggregates;

        public void Attach(IAggregateRoot aggregate)
        {
            attachedAggregates.FirstOrNothing(a => a.GetId() == aggregate.GetId())
                .ToResult("No duplicate")
                .OnFailure(() => attachedAggregates.Add(aggregate));
        }

        public Result<T> GetById<T>(Guid id)
            where T : class, IAggregateRoot
        {
            return this.attachedAggregates.FirstOrNothing(a => a.GetId() == id)
                .ToResult("No aggregate attached")
                .Map(a => a as T);
        }
    }
}