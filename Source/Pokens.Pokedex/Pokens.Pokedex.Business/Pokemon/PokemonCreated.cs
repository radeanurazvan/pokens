using System.Collections.Generic;
using System.Linq;
using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Pokedex.Business
{
    internal sealed class PokemonCreated : IBusMessage
    {
        private PokemonCreated()
        {
        }

        public PokemonCreated(Pokemon pokemon)
            : this()
        {
            Id = pokemon.Id;
            Name = pokemon.Name;
            Health = pokemon.Stats.Health;
            Defense = pokemon.Stats.Defense;
            DodgeChance = pokemon.Stats.DodgeChance;
            AttackPower = pokemon.Stats.AttackPower;
            CriticalStrikeChance = pokemon.Stats.CriticalStrikeChance;
            Abilities = pokemon.Abilities.Select(a => new CreatedPokemonAbility(a));
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public int Health { get; private set; }

        public int Defense { get; private set; }

        public float DodgeChance { get; private set; }

        public int AttackPower { get; private set; }

        public float CriticalStrikeChance { get; private set; }

        public IEnumerable<CreatedPokemonAbility> Abilities { get; private set; }

        internal sealed class CreatedPokemonAbility
        {
            private CreatedPokemonAbility()
            {
            }

            public CreatedPokemonAbility(Ability ability)
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