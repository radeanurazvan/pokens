using System;
using System.Collections.Generic;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    public sealed class StarterPokemonChosenEvent : IIntegrationEvent
    {
        private StarterPokemonChosenEvent()
        {
        }

        public string PokemonId { get; private set; }

        public string DefinitionName { get; private set; }

        public int Health { get; private set; }

        public int Defense { get; private set; }

        public float DodgeChance { get; private set; }

        public int AttackPower { get; private set; }

        public float CriticalStrikeChance { get; private set; }

        public IEnumerable<StarterPokemonAbility> Abilities { get; private set; }

        public sealed class StarterPokemonAbility
        {
            private StarterPokemonAbility()
            {
            }

            public Guid Id { get; private set; }

            public string Name { get; private set; }

            public string Description { get; private set; }

            public int Damage { get; private set; }

            public int RequiredLevel { get; private set; }

            public int Cooldown { get; private set; }
        }
    }
}