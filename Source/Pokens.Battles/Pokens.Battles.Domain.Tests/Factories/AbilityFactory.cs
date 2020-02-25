using System;

namespace Pokens.Battles.Domain.Tests
{
    internal static class AbilityFactory
    {
        public static Ability WithCooldown(int cooldown) => new Ability(Guid.NewGuid(), "Ability", "", 1, 0, cooldown);

        public static Ability WithRequiredLevel(int requiredLevel) => new Ability(Guid.NewGuid(), "Ability", "", 1, requiredLevel, 0);
    }
}