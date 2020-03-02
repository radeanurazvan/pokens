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

        public PokemonInFight(Guid pokemonId, DefensiveStats defensive, OffensiveStats offensive)
            : this()
        {
            Id = pokemonId;
            Defensive = defensive;
            Offensive = offensive;
        }

        public Guid Id { get; private set; }

        public DefensiveStats Defensive { get; private set; }

        public OffensiveStats Offensive { get; private set; }

        public bool HasFainted => Defensive.Health <= 0;

        internal IEnumerable<AbilityOnCooldown> Cooldowns => this.abilitiesOnCooldown;

        internal void DecrementCooldowns()
        {
            abilitiesOnCooldown.ForEach(a => a.Decrement());
            abilitiesOnCooldown.Where(a => a.CooldownFinished).ToList()
                .ForEach(a => abilitiesOnCooldown.Remove(a));
        }

        public Result Use(Ability ability)
        {
            return Result.SuccessIf(CanUse(ability), Messages.AbilityIsOnCooldown)
                .Bind(() => abilitiesOnCooldown.FirstOrNothing(a => a.AbilityId == ability.Id).ToResult("Ability not on cooldown yet"))
                .OnFailureCompensate(() => AbilityOnCooldown.From(ability))
                .Tap(ac => abilitiesOnCooldown.Add(ac));
        }

        public bool CanUse(Ability ability) => !HasCooldownFor(ability.Id);

        private bool HasCooldownFor(Guid ability) => abilitiesOnCooldown.Any(a => a.AbilityId == ability && !a.CooldownFinished);

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