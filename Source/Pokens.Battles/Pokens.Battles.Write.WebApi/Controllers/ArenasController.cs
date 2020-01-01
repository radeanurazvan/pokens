using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokens.Battles.Business;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Infrastructure;

namespace Pokens.Battles.Write.WebApi.Controllers
{
    [Authorize]
    [Route("api/v1/arenas")]
    public sealed class ArenasController : ControllerBase
    {
        private readonly IIdentifiedUser user;
        private readonly IMediator mediator;

        public ArenasController(IIdentifiedUser user, IMediator mediator)
        {
            this.user = user;
            this.mediator = mediator;
        }

        [HttpPatch("{id:Guid}/enrollments")]
        public async Task<IActionResult> Enroll([FromRoute] Guid id)
        {
            var command = new EnrollTrainerCommand(id, user.Id.Value);
            var result = await mediator.Send(command);

            return result.ToActionResult(NoContent);
        }

        [HttpDelete("{id:Guid}/enrollments")]
        public async Task<IActionResult> EndEnrollment([FromRoute] Guid id)
        {
            var command = new EndTrainerEnrollmentCommand(id, user.Id.Value);
            var result = await mediator.Send(command);

            return result.ToActionResult(NoContent);
        }

        [HttpPatch("{id:Guid}/trainers/{challengedId:Guid}/challenges")]
        public async Task<IActionResult> ChallengeTrainer([FromRoute] Guid id, [FromRoute] Guid challengedId)
        {
            var command = new ChallengeTrainerCommand(id, this.user.Id.Value, challengedId);
            var result = await mediator.Send(command);

            return result.ToActionResult(NoContent);
        }
    }
}