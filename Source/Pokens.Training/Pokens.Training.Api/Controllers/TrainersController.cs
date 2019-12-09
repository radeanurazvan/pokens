using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pokens.Training.Business;
using Pomelo.Kernel.Domain;

namespace Pokens.Training.Api.Controllers
{
    [Route("api/v1/trainers")]
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
    }
}