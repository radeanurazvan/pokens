using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerLeftArenaEvent : IDomainEvent
    {
        public TrainerLeftArenaEvent()
        {
            LeftAt = TimeProvider.Instance().UtcNow;
        }

        public DateTime LeftAt { get; private set; }
    }
}