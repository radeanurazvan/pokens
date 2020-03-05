using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public sealed class PlayerCooldownChangedEvent : IDomainEvent
    {
        private PlayerCooldownChangedEvent()
        {
        }

        public PlayerCooldownChangedEvent(Guid battleId, Guid playerId, Guid abilityId, int cooldown)
            : this()
        {
            BattleId = battleId;
            PlayerId = playerId; 
            AbilityId = abilityId;
            Cooldown = cooldown;
        }

        public Guid BattleId { get; private set; }

        public Guid PlayerId { get; private set; }

        public Guid AbilityId { get; private set; }

        public int Cooldown { get; private set; }
    }
}