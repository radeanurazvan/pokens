using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemons.Trainers.Business;
using Pokens.Trainers.Domain;
using Pomelo.Kernel.Infrastructure;

namespace Pokens.Trainers.Api.Controllers
{
    [Route("api/v1/trainers")]
    public sealed class TrainersController : ControllerBase
    {
        private readonly IMediator mediator;

        public TrainersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> Register([FromBody] RegisterTrainerModel model)
        {
            var command = new RegisterTrainerCommand(model.Name, model.Email, model.Password);
            var result = await mediator.Send(command);

            return result.ToActionResult(() => StatusCode(StatusCodes.Status201Created));
        }

        [HttpPost("token")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateTrainerModel model)
        {
            var command = new AuthenticateTrainerCommand(model.Email, model.Password);
            var result = await mediator.Send(command);

            return result.ToActionResult(() => Ok(new GenericApiResult<AuthenticationToken>(result)));
        }
    }
}