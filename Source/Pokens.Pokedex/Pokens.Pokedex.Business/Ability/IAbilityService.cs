using System.Collections.Generic;

namespace Pokens.Pokedex.Business
{
    public interface IAbilityService
    {
        IEnumerable<AbilityModel> GetAll();

        void Create(string name, string description, int damage, int requiredLevel, int cooldown);
    }
}