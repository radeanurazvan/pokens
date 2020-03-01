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
            Name = "Tail Whip",
            Damage = 1,
            Description = "Hits the opponent with the tail"
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