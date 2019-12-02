using System.Collections.Generic;
using Pomelo.Kernel.Domain;

namespace Pokens.Pokedex.Domain
{
    public sealed class Pokemon : DocumentEntity
    {
        public string Name { get; set; }

        public Stats Stats { get; set; }

        public bool IsStarter { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();

        public ICollection<Ability> Abilities { get; set; } = new List<Ability>();
    }

}