using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokens.Pokedex.Business;
using Pokens.Pokedex.Api.Extensions;
using CSharpFunctionalExtensions;

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
