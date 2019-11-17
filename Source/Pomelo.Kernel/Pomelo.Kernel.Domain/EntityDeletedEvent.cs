using System;

namespace Pomelo.Kernel.Domain
{
    public abstract class EntityDeletedEvent : IDomainEvent
    {
        protected EntityDeletedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}