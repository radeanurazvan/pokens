using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public class PlayerUsedAbilityEvent : IDomainEvent
    {
        private PlayerUsedAbilityEvent()
        {
        }

        public PlayerUsedAbilityEvent(Ability ability, int damageDealt)
            : this()
        {
            Ability = ability;
            DamageDealt = damageDealt;
        }

        public Ability Ability { get; private set; }

        public int DamageDealt { get; private set; }
    }
}