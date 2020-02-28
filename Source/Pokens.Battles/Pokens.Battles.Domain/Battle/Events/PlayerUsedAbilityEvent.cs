using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public class PlayerUsedAbilityEvent : IDomainEvent
    {
        private PlayerUsedAbilityEvent()
        {
        }

        public PlayerUsedAbilityEvent(Guid playerId, Ability ability, int damageDealt)
            : this()
        {
            PlayerId = playerId;
            Ability = ability;
            DamageDealt = damageDealt;
        }

        public Guid PlayerId { get; private set; }

        public Ability Ability { get; private set; }

        public int DamageDealt { get; private set; }
    }
}