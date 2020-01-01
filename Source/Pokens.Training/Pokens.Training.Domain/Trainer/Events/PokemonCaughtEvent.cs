using Pomelo.Kernel.Domain;

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
        }

        public string PokemonId { get; private set; }

        public string DefinitionName { get; private set; }

        public int Health { get; private set; }

        public int Defense { get; private set; }

        public float DodgeChance { get; private set; }

        public int AttackPower { get; private set; }

        public float CriticalStrikeChance { get; private set; }
    }
}