using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerLeftArenaEvent : IDomainEvent
    {
        public TrainerLeftArenaEvent()
        {
            LeftAt = DateTimeProvider.Instance().UtcNow;
        }

        public DateTime LeftAt { get; private set; }
    }
}