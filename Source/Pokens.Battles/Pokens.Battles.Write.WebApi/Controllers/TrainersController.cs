using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pokens.Battles.Business;
using Pokens.Battles.Write.WebApi.Models;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Http;

namespace Pokens.Battles.Write.WebApi.Controllers
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

        [HttpPatch("me/battles/{battleId:Guid}")]
        public async Task<IActionResult> UseAbility([FromRoute] Guid battleId, [FromBody] UseAbilityModel model)
        {
            var command = new UseAbilityCommand(battleId, user.Id.Value, model.AbilityId);
            var result = await mediator.Send(command);

            return result.ToActionResult(NoContent);
        }
    }
}