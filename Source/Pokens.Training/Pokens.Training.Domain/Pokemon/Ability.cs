﻿namespace Pokens.Training.Domain
{
    public sealed class Ability
    {
        private Ability()
        {
        }

        public Ability(string id, string name, string description, int damage, int requiredLevel, int cooldown, byte[] image)
            : this()
        {
            Id = id;
            Name = name;
            Description = description;
            Damage = damage;
            RequiredLevel = requiredLevel;
            Cooldown = cooldown;
            Image = image;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public int Damage { get; private set; }

        public int RequiredLevel { get; private set; }

        public int Cooldown { get; private set; }

        public byte[] Image { get; private set; }
    }
}