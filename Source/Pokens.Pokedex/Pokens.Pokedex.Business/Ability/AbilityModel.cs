using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Business
{
    public sealed class AbilityModel
    {
        public AbilityModel(Ability ability)
        {
            Id = ability.Id;
            Name = ability.Name;
            Description = ability.Description;
            RequiredLevel = ability.RequiredLevel;
            Damage = ability.Damage;
            Cooldown = ability.Cooldown;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int RequiredLevel { get; set; }

        public int Damage { get; set; }

        public int Cooldown { get; set; }

        public string Summary => $"{Name}, deals {Damage} {nameof(Damage)}, requires level {RequiredLevel}, {Cooldown} rounds {nameof(Cooldown)}";
        
    }
}