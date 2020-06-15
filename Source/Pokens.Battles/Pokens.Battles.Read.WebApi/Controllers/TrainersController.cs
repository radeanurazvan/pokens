using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokens.Battles.Read.Domain;
using Pomelo.Kernel.DataSynchronization;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Read.WebApi
{
    [Authorize]
    [Route("api/v1/trainers")]
    public sealed class TrainersController : ControllerBase
    {
        private readonly IIdentifiedUser user;
        private readonly ISyncReadRepository<TrainerModel> repository;

        public TrainersController(ISyncReadRepository<TrainerModel> repository, IIdentifiedUser user)
        {
            this.repository = repository;
            this.user = user;
        }

        [HttpGet("me/challenges/received")]
        public async Task<IActionResult> GetReceivedChallenges()
        {
            var trainerOrNothing = await repository.GetById(user.Property("Id").Value.ToString());
            var challenges = trainerOrNothing.Map(t => t.Challenges.Where(c => c.ChallengerId != t.Id))
                .Unwrap(new List<ChallengeModel>());

            return Ok(challenges);
        }

        [HttpGet("me/challenges/sent")]
        public async Task<IActionResult> GetSentChallenges()
        {
            var trainerOrNothing = await repository.GetById(user.Property("Id").Value.ToString());
            var challenges = trainerOrNothing.Map(t => t.Challenges.Where(c => c.ChallengerId == t.Id))
                .Unwrap(new List<ChallengeModel>());

            return Ok(challenges);
        }
    }
}