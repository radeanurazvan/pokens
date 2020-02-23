using System.Collections.Generic;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    public sealed class PokemonCaughtEvent : IIntegrationEvent
    {
        private PokemonCaughtEvent()
        {
        }

        public string PokemonId { get; private set; }

        public string DefinitionName { get; private set; }

        public int Health { get; private set; }

        public int Defense { get; private set; }

        public float DodgeChance { get; private set; }

        public int AttackPower { get; private set; }

        public float CriticalStrikeChance { get; private set; }

        public IEnumerable<CaughtPokemonAbility> Abilities { get; private set; }

        public sealed class CaughtPokemonAbility
        {
            private CaughtPokemonAbility()
            {
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