using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Pokedex.Business
{
    internal sealed class PokemonStatsChanged : IBusMessage
    {
        private PokemonStatsChanged()
        {
        }

        public PokemonStatsChanged(Pokemon pokemon)
            : this()
        {
            PokemonId = pokemon.Id;
            Health = pokemon.Stats.Health;
            Defense = pokemon.Stats.Defense;
            DodgeChance = pokemon.Stats.DodgeChance;
            AttackPower = pokemon.Stats.AttackPower;
            CriticalStrikeChance = pokemon.Stats.CriticalStrikeChance;
        }

        public string PokemonId { get; private set; }

        public int Health { get; private set; }

        public int Defense { get; private set; }

        public float DodgeChance { get; private set; }

        public int AttackPower { get; private set; }

        public float CriticalStrikeChance { get; private set; }

    }
}