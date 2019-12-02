using System.Collections.Generic;
using System.Linq;
using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Domain;

namespace Pokens.Pokedex.Business
{
    internal sealed class AbilityService : IAbilityService
    {
        private readonly ICollectionRepository repository;

        public AbilityService(ICollectionRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<AbilityModel> GetAll()
        {
            return this.repository.GetAll<Ability>().Select(a => new AbilityModel(a));
        }

        public void Create(string name, string description, int damage, int requiredLevel, int cooldown)
        {
            var ability = new Ability
            {
                Name = name,
                Description = description,
                Damage = damage,
                RequiredLevel = requiredLevel,
                Cooldown = cooldown
            };

            this.repository.Add(ability);
        }
    }
}