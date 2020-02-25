using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class PlayerTookTurnEvent : IDomainEvent
    {
        private PlayerTookTurnEvent()
        {
        }

        public PlayerTookTurnEvent(Guid abilityId) 
            : this()
        {
            AbilityId = abilityId;
            TakenAt = TimeProvider.Instance().UtcNow;
        }

        public Guid AbilityId { get; private set; }

        public DateTime TakenAt { get; private set; }
    }
}