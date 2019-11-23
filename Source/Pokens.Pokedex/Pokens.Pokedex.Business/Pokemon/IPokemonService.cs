using System.Collections.Generic;
using System.Threading.Tasks;
using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Business
{
    public interface IPokemonService
    {
        IEnumerable<PokemonModel> GetAll();

        Task Create(string name, Stats stats, IEnumerable<string> abilities);
        
        Task ChangeStats(string pokemonId, Stats newStats);
        
        Task ChangeAbilities(string pokemonId, IEnumerable<string> abilitiesIds);
    }
}