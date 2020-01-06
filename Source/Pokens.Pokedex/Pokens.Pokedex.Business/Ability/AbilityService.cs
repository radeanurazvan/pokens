using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<AbilityModel>> GetAll()
        {
            return (await this.repository.GetAll<Ability>()).Select(a => new AbilityModel(a));
        }

        public Task Create(string name, string description, int damage, int requiredLevel, int cooldown)
        {
            var ability = new Ability
            {
                Name = name,
                Description = description,
                Damage = damage,
                RequiredLevel = requiredLevel,
                Cooldown = cooldown
            };

            return this.repository.Add(ability);
        }

        public async Task AddImage(string id, byte[] content, string imageName)
        {
            var abilityOrNothing = await this.repository.FindOne<Ability>(p => p.Id == id);
            if (abilityOrNothing.HasNoValue)
            {
                return;
            }
            var ability = abilityOrNothing.Value;

            var img = new Image(imageName, content);
            ability.Image = img;

            await this.repository.Update(ability);
        }
    }
}