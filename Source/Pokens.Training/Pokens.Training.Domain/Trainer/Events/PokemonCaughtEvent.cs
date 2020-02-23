using System.Collections.Generic;
using System.Linq;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Domain
{
    internal sealed class PokemonCaughtEvent : IDomainEvent
    {
        private PokemonCaughtEvent()
        {
        }

        public PokemonCaughtEvent(string pokemonId, PokemonDefinition definition)
            : this()
        {
            PokemonId = pokemonId;
            DefinitionName = definition.Name;
            Health = definition.Stats.Health;
            Defense = definition.Stats.Defense;
            DodgeChance = definition.Stats.DodgeChance;
            AttackPower = definition.Stats.AttackPower;
            CriticalStrikeChance = definition.Stats.CriticalStrikeChance;
            Abilities = definition.Abilities.Select(a => new CaughtPokemonAbility(a)).ToList();
        }

        public string PokemonId { get; private set; }

        public string DefinitionName { get; private set; }

        public int Health { get; private set; }

        public int Defense { get; private set; }

        public float DodgeChance { get; private set; }

        public int AttackPower { get; private set; }

        public float CriticalStrikeChance { get; private set; }

        public IEnumerable<CaughtPokemonAbility> Abilities { get; private set; }

        internal sealed class CaughtPokemonAbility
        {
            private CaughtPokemonAbility()
            {
            }

            public CaughtPokemonAbility(Ability ability)
                : this()
            {
                Id = ability.Id;
                Name = ability.Name;
                Description = ability.Description;
                Damage = ability.Damage;
                RequiredLevel = ability.RequiredLevel;
                Cooldown = ability.Cooldown;
            }

            public string Id { get; private set; }

            public string Name { get; private set; }

            public string Description { get; private set; }

            public int Damage { get; private set; }

            public int RequiredLevel { get; private set; }

            public int Cooldown { get; private set; }
        }
    }
}