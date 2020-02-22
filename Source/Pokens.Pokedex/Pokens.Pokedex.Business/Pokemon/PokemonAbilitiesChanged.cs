using System.Collections.Generic;
using System.Linq;
using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Pokedex.Business
{
    internal class PokemonAbilitiesChanged : IDomainEvent
    {
        private PokemonAbilitiesChanged()
        {
        }

        public PokemonAbilitiesChanged(Pokemon pokemon)
            : this()
        {
            PokemonId = pokemon.Id;
            Abilities = pokemon.Abilities.Select(a => new ChangedPokemonAbility(a));
        }

        public string PokemonId { get; private set; }

        public IEnumerable<ChangedPokemonAbility> Abilities { get; private set; }

        internal sealed class ChangedPokemonAbility
        {
            private ChangedPokemonAbility()
            {
            }

            public ChangedPokemonAbility(Ability ability)
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