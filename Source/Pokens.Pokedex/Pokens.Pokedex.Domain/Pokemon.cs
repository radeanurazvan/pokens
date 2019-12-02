using System.Collections.Generic;

namespace Pokens.Pokedex.Domain
{
    public sealed class Pokemon : PokedexEntity
    {
        public string Name { get; set; }

        public Stats Stats { get; set; }

        public bool IsStarter { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();

        public ICollection<Ability> Abilities { get; set; } = new List<Ability>();
    }

}