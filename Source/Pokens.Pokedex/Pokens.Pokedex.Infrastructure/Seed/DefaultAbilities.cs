using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Infrastructure.Seed
{
    internal static class DefaultAbilities
    {
        public static Ability Tackle { get; } = new Ability
        {
            Name = "Tackle",
            Damage = 15,
            Description = "A physical attack in which the user charges, full body, into the foe.",
            Image = new Image("Tackle.png", ImageUrlToBytes(DefaultImages.Abilities.Tackle))
        };

        public static Ability VineWhip { get; } = new Ability
        {
            Name = "Vine Whip",
            Damage = 35,
            Description = "Strikes the foe with slender, whiplike vines.",
            Image = new Image("TakeDown.png", ImageUrlToBytes(DefaultImages.Abilities.VineWhip)),
            RequiredLevel = 3,
            Cooldown = 1
        };

        public static Ability RazorLeaf { get; } = new Ability
        {
            Name = "Razor Leaf",
            Damage = 60,
            Description = "Sharp-edged leaves are launched to slash at the opposing team.",
            Image = new Image("TakeDown.png", ImageUrlToBytes(DefaultImages.Abilities.RazorLeaf)),
            RequiredLevel = 5,
            Cooldown = 3
        };

        public static Ability PowerWhip { get; } = new Ability
        {
            Name = "Power Whip",
            Damage = 90,
            Description = "The user violently whirls its vines or tentacles to harshly lash the foe.",
            Image = new Image("TakeDown.png", ImageUrlToBytes(DefaultImages.Abilities.PowerWhip)),
            RequiredLevel = 10,
            Cooldown = 5
        };

        public static Ability Scratch { get; } = new Ability
        {
            Name = "Scratch",
            Damage = 12,
            Description = "Scratches the foe with sharp claws.",
            Image = new Image("Scratch.png", ImageUrlToBytes(DefaultImages.Abilities.CharmanderScratch))
        };

        public static Ability Ember { get; } = new Ability
        {
            Name = "Ember",
            Damage = 40,
            Description = "A weak fire attack that may inflict a burn.",
            Image = new Image("Ember.png", ImageUrlToBytes(DefaultImages.Abilities.CharmanderEmber)),
            Cooldown = 1,
            RequiredLevel = 3
        };

        public static Ability FireFang { get; } = new Ability
        {
            Name = "Fire Fang",
            Damage = 50,
            Description = "The user bites with flame-cloaked fangs. It may also make the foe flinch or sustain a burn.",
            Image = new Image("FireFang.png", ImageUrlToBytes(DefaultImages.Abilities.CharmanderFireFang)),
            Cooldown = 1,
            RequiredLevel = 6
        };

        public static Ability FireBurst { get; } = new Ability
        {
            Name = "Fire Burst",
            Damage = 70,
            Description = "The user attacks the target with a bursting flame",
            Image = new Image("FireBurst.png", ImageUrlToBytes(DefaultImages.Abilities.CharmanderFireBurst)),
            Cooldown = 3,
            RequiredLevel = 10
        };

        public static Ability FlameThrower { get; } = new Ability
        {
            Name = "Flame Thrower",
            Damage = 100,
            Description = "A powerful fire attack that may inflict a burn",
            Image = new Image("FlameThrower.png", ImageUrlToBytes(DefaultImages.Abilities.CharmanderFlameThrower)),
            Cooldown = 5,
            RequiredLevel = 15
        };

        public static Ability TailWhip { get; } = new Ability
        {
            Name = "TailWhip",
            Damage = 1,
            Description = "Hits the opponent with the tail",
            Image = new Image("TailWhip.png", ImageUrlToBytes(DefaultImages.Abilities.TailWhip)),
            Cooldown = 8,
        };

        public static Ability Overgrow { get; } = new Ability
        {
            Name = "Overgrow",
            Damage = 2,
            Description = "Raises Attack when HP is low.",
            Image = new Image("Overgrow.png", ImageUrlToBytes(DefaultImages.Abilities.Overgrow)),
            Cooldown = 20,
        };

        public static Ability Chlorophyll { get; } = new Ability
        {
            Name = "Chlorophyll",
            Damage = 4,
            Description = "Boosts attack frequency if the weather is sunny.",
            Image = new Image("Chlorophyll.png", ImageUrlToBytes(DefaultImages.Abilities.Chlorophyll)),
            Cooldown = 5,
            RequiredLevel = 15
        };

        public static Ability ThickFat { get; } = new Ability
        {
            Name = "ThickFat",
            Damage = 1,
            Description = "Resistant to Fire- and Ice-type moves.",
            Image = new Image("ThickFat.png", ImageUrlToBytes(DefaultImages.Abilities.ThickFat))
        };

        public static Ability LightningRod { get; } = new Ability
        {
            Name = "LightningRod",
            Damage = 3,
            Description = "Absorbs all Electric-type moves to raise Attack.",
            Image = new Image("LightningRod.png", ImageUrlToBytes(DefaultImages.Abilities.LightningRod)),
            Cooldown = 10,
            RequiredLevel = 8
        };

        public static Ability Blaze { get; } = new Ability
        {
            Name = "Blaze",
            Damage = 1,
            Description = "Boosts the power of Fire-type moves when HP is low.",
            Image = new Image("Blaze.png", ImageUrlToBytes(DefaultImages.Abilities.Blaze))
        };

        public static Ability SolarPower { get; } = new Ability
        {
            Name = "SolarPower",
            Damage = 2,
            Description = "Boosts Special Attack in sunny weather, also loses HP.",
            Image = new Image("SolarPower.png", ImageUrlToBytes(DefaultImages.Abilities.SolarPower)),
            Cooldown = 12,
            RequiredLevel = 18
        };

        public static Ability Torrent { get; } = new Ability
        {
            Name = "Torrent",
            Damage = 3,
            Description = "Raises Attack when HP is low.",
            Image = new Image("Torrent.png", ImageUrlToBytes(DefaultImages.Abilities.Torrent)),
            Cooldown = 5,
            RequiredLevel = 6
        };

        public static Ability KeenEye { get; } = new Ability
        {
            Name = "KeenEye",
            Damage = 0,
            Description = "Prevents the Pokémon from losing health.",
            Image = new Image("KeenEye.png", ImageUrlToBytes(DefaultImages.Abilities.KeenEye)),
            Cooldown = 3,
        };


        public static IEnumerable<Ability> All => typeof(DefaultAbilities).GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => p.PropertyType == typeof(Ability))
            .Select(p => p.GetValue(null, null))
            .Select(p => p as Ability);

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