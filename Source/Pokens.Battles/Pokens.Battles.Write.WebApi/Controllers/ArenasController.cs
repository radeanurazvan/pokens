using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokens.Battles.Business;
using Pokens.Battles.Write.WebApi.Models;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Http;

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
            var command = new EnrollTrainerCommand(id, new Guid(user.Property("Id").Value));
            var result = await mediator.Send(command);

            return result.ToActionResult(NoContent);
        }

        [HttpDelete("{id:Guid}/enrollments")]
        public async Task<IActionResult> EndEnrollment([FromRoute] Guid id)
        {
            var command = new EndTrainerEnrollmentCommand(id, new Guid(user.Property("Id").Value));
            var result = await mediator.Send(command);

            return result.ToActionResult(NoContent);
        }

        [HttpPatch("{id:Guid}/trainers/{challengedId:Guid}/challenges")]
        public async Task<IActionResult> ChallengeTrainer([FromRoute] Guid id, [FromRoute] Guid challengedId, [FromBody] ChallengeTrainerModel model)
        {
            var command = new ChallengeTrainerCommand(id, new Guid(user.Property("Id").Value), model.ChallengerPokemonId, challengedId, model.ChallengedPokemonId);
            var result = await mediator.Send(command);

            return result.ToActionResult(NoContent);
        }

        [HttpPatch("{id:Guid}/trainers/me/challenges/{challengeId:Guid}")]
        public async Task<IActionResult> AcceptChallenge([FromRoute] Guid id, [FromRoute] Guid challengeId)
        {
            var command = new AcceptChallengeCommand(id, challengeId);
            var result = await mediator.Send(command);

            return result.ToActionResult(NoContent);
        }

        [HttpDelete("{id:Guid}/trainers/me/challenges/{challengeId:Guid}")]
        public async Task<IActionResult> RejectChallenge([FromRoute] Guid id, [FromRoute] Guid challengeId)
        {
            var command = new RejectChallengeCommand(new Guid(user.Property("Id").Value), challengeId);
            var result = await mediator.Send(command);

            return result.ToActionResult(NoContent);
        }
    }
}