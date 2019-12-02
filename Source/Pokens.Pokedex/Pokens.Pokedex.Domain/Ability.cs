﻿using Pomelo.Kernel.Domain;

namespace Pokens.Pokedex.Domain
{
    public sealed class Ability : DocumentEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Damage { get; set; }

        public int RequiredLevel { get; set; }

        public int Cooldown { get; set; }
    }
}