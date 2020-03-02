using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class PlayerCooldownChanged : IDomainEvent
    {
        private PlayerCooldownChanged()
        {
        }

        public PlayerCooldownChanged(Guid abilityId, int cooldown)
        {
            AbilityId = abilityId;
            Cooldown = cooldown;
        }

        public Guid AbilityId { get; private set; }

        public int Cooldown { get; private set; }
    }
}