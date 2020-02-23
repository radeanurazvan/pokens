using System;
using System.Collections.Generic;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class PokemonCreated : IIntegrationEvent
    {
        private PokemonCreated()
        {
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int Health { get; private set; }

        public int Defense { get; private set; }

        public float DodgeChance { get; private set; }

        public int AttackPower { get; private set; }

        public float CriticalStrikeChance { get; private set; }

        public double CatchRate { get; private set; }

        public IEnumerable<CreatedPokemonAbility> Abilities { get; private set; }

        internal sealed class CreatedPokemonAbility
        {
            private CreatedPokemonAbility()
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