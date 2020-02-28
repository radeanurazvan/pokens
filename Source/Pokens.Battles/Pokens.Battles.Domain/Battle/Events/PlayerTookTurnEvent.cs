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

        public PlayerTookTurnEvent(Guid playerId, Guid abilityId) 
            : this()
        {
            PlayerId = playerId;
            AbilityId = abilityId;
            TakenAt = TimeProvider.Instance().UtcNow;
        }

        public Guid PlayerId { get; private set; }

        public Guid AbilityId { get; private set; }

        public DateTime TakenAt { get; private set; }
    }
}