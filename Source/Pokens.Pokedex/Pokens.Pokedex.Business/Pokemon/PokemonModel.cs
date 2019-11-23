using System.Collections.Generic;
using System.Linq;
using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Business
{
    public sealed class PokemonModel
    {
        public PokemonModel(Pokemon pokemon)
        {
            Id = pokemon.Id;
            Name = pokemon.Name;
            Stats = new StatsModel(pokemon.Stats);
            Abilities = pokemon.Abilities.Select(a => new AbilityModel(a));
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public StatsModel Stats { get; set; }

        public IEnumerable<AbilityModel> Abilities { get; set; }
    }

    public sealed class StatsModel
    {
        public StatsModel(Stats pokemonStats)
        {
            Health = pokemonStats.Health;
            Defense = pokemonStats.Defense;
            DodgeChance = pokemonStats.DodgeChance;
            AttackPower = pokemonStats.AttackPower;
            CriticalStrikeChance = pokemonStats.CriticalStrikeChance;
        }

        public int Health { get; set; }

        public int Defense { get; set; }

        public float DodgeChance { get; set; }

        public int AttackPower { get; set; }

        public float CriticalStrikeChance { get; set; }

        public string Summary => $"{nameof(Health)}: {Health}, {nameof(Defense)}: {Defense}, {nameof(DodgeChance)}: {DodgeChance}, {nameof(AttackPower)}: {AttackPower}, {nameof(CriticalStrikeChance)}: {CriticalStrikeChance}";
    }
}