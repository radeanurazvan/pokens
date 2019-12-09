using System.Collections.Generic;
using System.Threading.Tasks;
using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Business
{
    public interface IPokemonService
    {
        Task<IEnumerable<PokemonModel>> GetAll();

        Task<IEnumerable<StarterPokemonModel>> GetStarters();

        Task Create(string name, Stats stats, IEnumerable<string> abilities);
        
        Task ChangeStats(string pokemonId, Stats newStats);
        
        Task ChangeAbilities(string pokemonId, IEnumerable<string> abilitiesIds);

        Task ChangeStarter(string pokemonId);

        Task ChangeImages(string pokemonId, byte[] contentImage, string imageName);

        Task DeleteImage(string pokemonId, string imageId);

    }
}