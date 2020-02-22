using System.Collections.Generic;
using System.Linq;
using Pomelo.Kernel.Domain;

namespace Pokens.Pokedex.Domain
{
    public sealed class Pokemon : DocumentAggregateRoot
    {
        public Pokemon(string name, Stats stats, bool isStarter, Rate catchRate, IList<Ability> abilities)
        {
            Name = name;
            Stats = stats;
            IsStarter = isStarter;
            CatchRate = catchRate;
            Abilities = abilities;

            AddDomainEvent(new PokemonCreated(this));
        }

        public string Name { get; set; }

        public Stats Stats { get; set; }

        public bool IsStarter { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();

        public ICollection<Ability> Abilities { get; set; } = new List<Ability>();

        public Rate CatchRate { get; set; }

        public void ChangeStats(Stats stats)
        {
            Stats = stats;
            AddDomainEvent(new PokemonStatsChanged(this));
        }

        public void ChangeAbilities(IEnumerable<Ability> abilities)
        {
            Abilities = abilities.ToList();
            AddDomainEvent(new PokemonAbilitiesChanged(this));
        }

        public void ToggleStarter()
        {
            IsStarter = !IsStarter;
            AddDomainEvent(new PokemonStarterChanged(this));
        }

        public void AddImage(Image image)
        {
            Images.Add(image);
            AddDomainEvent(new PokemonImagesChanged(this));
        }

        public void DeleteImage(Image image)
        {
            Images.Remove(image);
            AddDomainEvent(new PokemonImagesChanged(this));
        }
    }
}