using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokens.Pokedex.Business;

namespace Pokens.Pokedex.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/pokemons")]
    public sealed class PokemonsController : ControllerBase
    {
        private readonly IPokemonService pokemonService;

        public PokemonsController(IPokemonService pokemonService)
        {
            this.pokemonService = pokemonService;
        }

        [HttpGet("starters")]
        public async Task<IActionResult> GetStarterPokemons()
        {
            var result = await pokemonService.GetStarters();
            return Ok(result);
        }

        [HttpGet("roulette")]
        public async Task<IActionResult> GetPokemonRoulette()
        {
            var result = await pokemonService.GetPokemonRoulette();
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await pokemonService.GetAll();
            return Ok(result);
        }
    }
}
