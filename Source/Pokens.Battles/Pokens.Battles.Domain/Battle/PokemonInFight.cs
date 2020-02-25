using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Common;

namespace Pokens.Battles.Domain
{
    internal sealed class PokemonInFight
    {
        private readonly ICollection<AbilityOnCooldown> abilitiesOnCooldown = new List<AbilityOnCooldown>();

        private PokemonInFight()
        {
        }

        public PokemonInFight(DefensiveStats defensive, OffensiveStats offensive)
            : this()
        {
            Defensive = defensive;
            Offensive = offensive;
        }

        public DefensiveStats Defensive { get; private set; }

        public OffensiveStats Offensive { get; private set; }

        public bool HasFainted => Defensive.Health <= 0;

        internal void DecrementCooldowns()
        {
            abilitiesOnCooldown.ForEach(a => a.Decrement());
            abilitiesOnCooldown.Where(a => a.CooldownFinished).ToList()
                .ForEach(a => abilitiesOnCooldown.Remove(a));
        }

        public Result Use(Ability ability)
        {
            return Result.SuccessIf(CanUse(ability), Messages.AbilityIsOnCooldown)
                .Bind(() => AbilityOnCooldown.From(ability))
                .Tap(ac => abilitiesOnCooldown.Add(ac));
        }

        public bool CanUse(Ability ability) => !HasCooldownFor(ability.Id);

        private bool HasCooldownFor(Guid ability) => abilitiesOnCooldown.Any(a => a.AbilityId == ability);

        public void TakeHit(int hitDamage)
        {
            Defensive = Defensive.WithLessHealth(hitDamage);
        }
    }

    internal sealed class AbilityOnCooldown
    {
        private AbilityOnCooldown()
        {
        }

        public static Result<AbilityOnCooldown> From(Ability ability)
        {
            return ability.EnsureExists(Messages.InvalidAbility)
                .Ensure(a => a.Cooldown > 0, "Ability has no cooldown")
                .Map(a => new AbilityOnCooldown {AbilityId = ability.Id, Left = ability.Cooldown});
        }

        public Guid AbilityId { get; private set; }

        public int Left { get; private set; }

        public bool CooldownFinished => Left <= 0;

        public void Decrement() => Left--;
    }
}