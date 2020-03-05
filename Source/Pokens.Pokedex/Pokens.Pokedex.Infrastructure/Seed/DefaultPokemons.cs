using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Infrastructure.Seed
{
    internal static class DefaultPokemons
    {
        public static DefaultPokemon Pikachu { get; } = new DefaultPokemon
        {
            Name = "Pikachu",
            CatchRate = 35.2,
            Abilities = new List<Ability> { DefaultAbilities.TailWhip, DefaultAbilities.LightningRod },
            Images = new List<Image>{new Image("Pikachu.png", ImageUrlToBytes(DefaultImages.Pokemons.Pikachu))},
            Stats = new Stats
            {
                Health = 35,
                AttackPower = 55,
                Defense = 40,
                DodgeChance = 80,
                CriticalStrikeChance = 20
            }
        };

        public static DefaultPokemon Bulbasaur { get; } = new DefaultPokemon
        {
            Name = "Bulbasaur",
            CatchRate = 11.9,
            Abilities = new List<Ability> { DefaultAbilities.Tackle, DefaultAbilities.VineWhip, DefaultAbilities.RazorLeaf, DefaultAbilities.PowerWhip },
            Images = new List<Image> { new Image("Bulbasaur.png", ImageUrlToBytes(DefaultImages.Pokemons.Bulbasaur)) },
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

        public static DefaultPokemon Squirtle { get; } = new DefaultPokemon
        {
            Name = "Squirtle",
            CatchRate = 11.9,
            Abilities = new List<Ability> { DefaultAbilities.Scratch },
            Images = new List<Image> { new Image("Squirtle.png", ImageUrlToBytes(DefaultImages.Pokemons.Squirtle)) },
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

        public static DefaultPokemon Charmander { get; } = new DefaultPokemon
        {
            Name = "Charmander",
            CatchRate = 11.9,
            Abilities = new List<Ability> { DefaultAbilities.Scratch, DefaultAbilities.Ember, DefaultAbilities.FireFang, DefaultAbilities.FireBurst, DefaultAbilities.FlameThrower },
            Images = new List<Image> { new Image("Charmander.png", ImageUrlToBytes(DefaultImages.Pokemons.Charmander)) },
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

        public static DefaultPokemon Venusaur { get; } = new DefaultPokemon
        {
            Name = "Venusaur",
            CatchRate = 8.9,
            Abilities = new List<Ability> { DefaultAbilities.Overgrow, DefaultAbilities.Chlorophyll, DefaultAbilities.ThickFat },
            Images = new List<Image> { new Image("Venusaur.png", ImageUrlToBytes(DefaultImages.Pokemons.Venusaur)) },
            Stats = new Stats
            {
                Health = 69,
                AttackPower = 52,
                Defense = 43,
                DodgeChance = 35,
                CriticalStrikeChance = 12
            },
            IsStarter = false
        };

        public static DefaultPokemon Charmeleon { get; } = new DefaultPokemon
        {
            Name = "Charmeleon",
            CatchRate = 16.9,
            Abilities = new List<Ability> { DefaultAbilities.Blaze, DefaultAbilities.SolarPower },
            Images = new List<Image> { new Image("Charmeleon.png", ImageUrlToBytes(DefaultImages.Pokemons.Charmeleon)) },
            Stats = new Stats
            {
                Health = 29,
                AttackPower = 72,
                Defense = 30,
                DodgeChance = 37,
                CriticalStrikeChance = 18
            },
            IsStarter = false
        };

        public static DefaultPokemon Wartortle { get; } = new DefaultPokemon
        {
            Name = "Wartortle",
            CatchRate = 13.9,
            Abilities = new List<Ability> { DefaultAbilities.Scratch, DefaultAbilities.Torrent },
            Images = new List<Image> { new Image("Wartortle.png", ImageUrlToBytes(DefaultImages.Pokemons.Wartortle)) },
            Stats = new Stats
            {
                Health = 64,
                AttackPower = 38,
                Defense = 85,
                DodgeChance = 25,
                CriticalStrikeChance = 12
            },
            IsStarter = false
        };

        public static DefaultPokemon Caterpie { get; } = new DefaultPokemon
        {
            Name = "Caterpie",
            CatchRate = 13.9,
            Abilities = new List<Ability> { DefaultAbilities.Chlorophyll, DefaultAbilities.PowerWhip },
            Images = new List<Image> { new Image("Caterpie.png", ImageUrlToBytes(DefaultImages.Pokemons.Caterpie)) },
            Stats = new Stats
            {
                Health = 34,
                AttackPower = 68,
                Defense = 55,
                DodgeChance = 55,
                CriticalStrikeChance = 15
            },
            IsStarter = false
        };

        public static DefaultPokemon Rattata { get; } = new DefaultPokemon
        {
            Name = "Rattata",
            CatchRate = 3.9,
            Abilities = new List<Ability> { DefaultAbilities.TailWhip, DefaultAbilities.PowerWhip },
            Images = new List<Image> { new Image("Rattata.png", ImageUrlToBytes(DefaultImages.Pokemons.Rattata)) },
            Stats = new Stats
            {
                Health = 24,
                AttackPower = 78,
                Defense = 25,
                DodgeChance = 35,
                CriticalStrikeChance = 35
            },
            IsStarter = false
        };

        public static DefaultPokemon Pidgeotto { get; } = new DefaultPokemon
        {
            Name = "Pidgeotto",
            CatchRate = 23.9,
            Abilities = new List<Ability> { DefaultAbilities.KeenEye, DefaultAbilities.SolarPower },
            Images = new List<Image> { new Image("Pidgeotto.png", ImageUrlToBytes(DefaultImages.Pokemons.Pidgeotto)) },
            Stats = new Stats
            {
                Health = 24,
                AttackPower = 78,
                Defense = 25,
                DodgeChance = 25,
                CriticalStrikeChance = 65
            },
            IsStarter = false
        };

        public static DefaultPokemon Tentacruel { get; } = new DefaultPokemon
        {
            Name = "Tentacruel",
            CatchRate = 23.9,
            Abilities = new List<Ability> { DefaultAbilities.Tackle, DefaultAbilities.RazorLeaf },
            Images = new List<Image> { new Image("Tentacruel.png", ImageUrlToBytes(DefaultImages.Pokemons.Tentacruel)) },
            Stats = new Stats
            {
                Health = 54,
                AttackPower = 38,
                Defense = 55,
                DodgeChance = 35,
                CriticalStrikeChance = 5
            },
            IsStarter = false
        };

        public static DefaultPokemon Golduck { get; } = new DefaultPokemon
        {
            Name = "Golduck",
            CatchRate = 30.9,
            Abilities = new List<Ability> { DefaultAbilities.Torrent, DefaultAbilities.Scratch },
            Images = new List<Image> { new Image("Golduck.png", ImageUrlToBytes(DefaultImages.Pokemons.Golduck)) },
            Stats = new Stats
            {
                Health = 74,
                AttackPower = 28,
                Defense = 25,
                DodgeChance = 15,
                CriticalStrikeChance = 8
            },
            IsStarter = false
        };

        public static DefaultPokemon Onix { get; } = new DefaultPokemon
        {
            Name = "Onix",
            CatchRate = 5.9,
            Abilities = new List<Ability> { DefaultAbilities.ThickFat, DefaultAbilities.Overgrow },
            Images = new List<Image> { new Image("Onix.png", ImageUrlToBytes(DefaultImages.Pokemons.Onix)) },
            Stats = new Stats
            {
                Health = 94,
                AttackPower = 18,
                Defense = 25,
                DodgeChance = 5,
                CriticalStrikeChance = 6
            },
            IsStarter = false
        };

        public static DefaultPokemon Golem { get; } = new DefaultPokemon
        {
            Name = "Golem",
            CatchRate = 5.9,
            Abilities = new List<Ability> { DefaultAbilities.ThickFat, DefaultAbilities.Chlorophyll },
            Images = new List<Image> { new Image("Golem.png", ImageUrlToBytes(DefaultImages.Pokemons.Golem)) },
            Stats = new Stats
            {
                Health = 94,
                AttackPower = 8,
                Defense = 45,
                DodgeChance = 25,
                CriticalStrikeChance = 3
            },
            IsStarter = false
        };

        public static DefaultPokemon Geodude { get; } = new DefaultPokemon
        {
            Name = "Geodude",
            CatchRate = 5.9,
            Abilities = new List<Ability> { DefaultAbilities.FlameThrower, DefaultAbilities.RazorLeaf },
            Images = new List<Image> { new Image("Geodude.png", ImageUrlToBytes(DefaultImages.Pokemons.Geodude)) },
            Stats = new Stats
            {
                Health = 44,
                AttackPower = 28,
                Defense = 25,
                DodgeChance = 35,
                CriticalStrikeChance = 13
            },
            IsStarter = false
        };

        public static IEnumerable<DefaultPokemon> All { get; } = new List<DefaultPokemon> { Pikachu, Charmander, Bulbasaur, Squirtle, 
            Venusaur, Charmeleon, Wartortle, Caterpie, Geodude, Golem, Onix, Golduck, Tentacruel, Pidgeotto, Rattata};

        private static byte[] ImageUrlToBytes(string url)
        {
            using var client = new HttpClient();
            var stream = client.GetStreamAsync(url).GetAwaiter().GetResult();

            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }

    internal class DefaultPokemon
    {
        public string Name { get; set; }
        public Rate CatchRate { get; set; }
        public List<Ability> Abilities { get; set; }
        public List<Image> Images { get; set; }
        public Stats Stats { get; set; }
        public bool IsStarter { get; set; }
    }
}