using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Business
{
    internal sealed class AbilityService : IAbilityService
    {
        private readonly IPokedexRepository repository;

        public AbilityService(IPokedexRepository repository)
        {
            this.repository = repository;
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