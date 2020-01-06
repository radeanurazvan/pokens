using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokens.Pokedex.Business
{
    public interface IAbilityService
    {
        Task<IEnumerable<AbilityModel>> GetAll();

        Task Create(string name, string description, int damage, int requiredLevel, int cooldown);

        Task AddImage(string id, byte[] content, string imageName);
    }
}