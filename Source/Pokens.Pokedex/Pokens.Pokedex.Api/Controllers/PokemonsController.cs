using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokens.Pokedex.Business;

namespace Pokens.Pokedex.Api.Controllers
{
    [Route("api/v1/pokemons")]
    [ApiController]
    public sealed class PokemonsController : ControllerBase
    {
        private readonly IPokemonService pokemonService;

        public PokemonsController(IPokemonService pokemonService)
        {
            this.pokemonService = pokemonService;
        }

        // GET: api/Pokemons/starters
        [HttpGet("starters")]
        public async Task<ActionResult<IEnumerable<PokemonModel>>> GetStarterPokemons()
        {
            var result = pokemonService.GetStarters();
            return new ActionResult<IEnumerable<PokemonModel>>(result);
        }
    }
}
