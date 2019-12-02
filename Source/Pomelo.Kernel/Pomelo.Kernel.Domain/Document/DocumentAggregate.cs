using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Pomelo.Kernel.Domain
{
    public abstract class DocumentAggregate : DocumentEntity
    {
        private readonly List<IDomainEvent> events = new List<IDomainEvent>();

        public IReadOnlyList<IDomainEvent> Events => events;

        protected Result AddDomainEvent(IDomainEvent @event)
        {
            return Maybe<IDomainEvent>.From(@event)
                .ToResult("Event cannot be null")
                .Tap(e => events.Add(e));
        }

        public void ClearEvents()
        {
            events.Clear();
        }

    }
}