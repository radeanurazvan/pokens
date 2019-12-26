using System.Collections.Generic;
using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Infrastructure.Seed
{
    internal static class DefaultAbilities
    {
        public static Ability Scratch { get; } = new Ability
        {
            Name = "Scratch",
            Damage = 1,
            Description = "Scratches the opponent"
        };
        
        public static Ability TailWhip { get; } = new Ability
        {
            Name = "Tail Whip",
            Damage = 1,
            Description = "Hits the opponent with the tail"
        };

        public static IEnumerable<Ability> All { get; } = new List<Ability> { Scratch, TailWhip };
    }
}