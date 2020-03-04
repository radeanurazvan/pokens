using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public sealed class PlayerTookTurnEvent : IDomainEvent
    {
        private PlayerTookTurnEvent()
        {
        }

        public PlayerTookTurnEvent(Guid battleId, Guid playerId, Guid abilityId) 
            : this()
        {
            BattleId = battleId;
            PlayerId = playerId;
            AbilityId = abilityId;
            TakenAt = TimeProvider.Instance().UtcNow;
        }

        public Guid BattleId { get; private set; }

        public Guid PlayerId { get; private set; }

        public Guid AbilityId { get; private set; }

        public DateTime TakenAt { get; private set; }
    }
}