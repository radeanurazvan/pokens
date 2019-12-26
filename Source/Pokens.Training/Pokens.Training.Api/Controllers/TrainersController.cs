using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokens.Training.Api.Models;
using Pokens.Training.Business;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Infrastructure;

namespace Pokens.Training.Api.Controllers
{
    [Route("api/v1/trainers")]
    [Authorize]
    public sealed class TrainersController : ControllerBase
    {
        private readonly IIdentifiedUser user;
        private readonly IMediator mediator;

        public TrainersController(IIdentifiedUser user, IMediator mediator)
        {
            this.user = user;
            this.mediator = mediator;
        }

        [HttpGet("me/pokemons")]
        public async Task<IActionResult> GetMyPokemons()
        {
            var query = new GetMyPokemonsQuery(user.Id.Value.ToString());
            var pokemons = await this.mediator.Send(query);

            if (pokemons.HasNoValue)
            {
                return NotFound();
            }
            return Ok(pokemons.Value);
        }

        [HttpPatch("me/starter")]
        public async Task<IActionResult> ChooseStarter([FromBody] ChooseStarterModel model)
        {
            var query = new ChooseStarterCommand(user.Id.Value.ToString(), model.PokemonId.ToString());
            var result = await this.mediator.Send(query);

            return result.ToActionResult(NoContent);
        }

        [HttpPatch("me/pokemons")]
        public async Task<IActionResult> CatchPokemon([FromBody] CatchPokemonModel model)
        {
            var query = new CatchPokemonCommand(user.Id.Value.ToString(), model.PokemonId.ToString());
            var result = await this.mediator.Send(query);

            return result.ToActionResult(NoContent);
        }
    }
}