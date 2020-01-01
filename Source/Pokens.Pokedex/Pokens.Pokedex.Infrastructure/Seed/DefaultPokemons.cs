using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Infrastructure.Seed
{
    internal static class DefaultPokemons
    {
        public static Pokemon Pikachu { get; } = new Pokemon
        {
            Name = "Pikachu",
            CatchRate = 35.2,
            Abilities = new List<Ability> { DefaultAbilities.TailWhip },
            Images = new List<Image>{new Image("Pikachu.png", ImageUrlToBytes(DefaultImages.Pikachu))},
            Stats = new Stats
            {
                Health = 35,
                AttackPower = 55,
                Defense = 40,
                DodgeChance = 80,
                CriticalStrikeChance = 20
            }
        };

        public static Pokemon Bulbasaur { get; } = new Pokemon
        {
            Name = "Bulbasaur",
            CatchRate = 11.9,
            Abilities = new List<Ability> { DefaultAbilities.Scratch },
            Images = new List<Image> { new Image("Bulbasaur.png", ImageUrlToBytes(DefaultImages.Bulbasaur)) },
            Stats = new Stats
            {
                Health = 45,
                AttackPower = 49,
                Defense = 49,
                DodgeChance = 40,
                CriticalStrikeChance = 10
            },
            IsStarter = true
        };

        public static Pokemon Squirtle { get; } = new Pokemon
        {
            Name = "Squirtle",
            CatchRate = 11.9,
            Abilities = new List<Ability> { DefaultAbilities.Scratch },
            Images = new List<Image> { new Image("Squirtle.png", ImageUrlToBytes(DefaultImages.Squirtle)) },
            Stats = new Stats
            {
                Health = 44,
                AttackPower = 48,
                Defense = 65,
                DodgeChance = 20,
                CriticalStrikeChance = 15
            },
            IsStarter = true
        };

        public static Pokemon Charmander { get; } = new Pokemon
        {
            Name = "Charmander",
            CatchRate = 11.9,
            Abilities = new List<Ability> { DefaultAbilities.Scratch },
            Images = new List<Image> { new Image("Charmander.png", ImageUrlToBytes(DefaultImages.Charmander)) },
            Stats = new Stats
            {
                Health = 39,
                AttackPower = 52,
                Defense = 43,
                DodgeChance = 35,
                CriticalStrikeChance = 12
            },
            IsStarter = true
        };

        public static IEnumerable<Pokemon> All { get; } = new List<Pokemon> { Pikachu, Charmander, Bulbasaur, Squirtle };

        private static byte[] ImageUrlToBytes(string url)
        {
            using var client = new HttpClient();
            var stream = client.GetStreamAsync(url).GetAwaiter().GetResult();

            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}