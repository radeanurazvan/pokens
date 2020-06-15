using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokens.Battles.Read.Domain;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.DataSynchronization;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Read.WebApi
{
    [Authorize]
    [Route("api/v1/battles")]
    public sealed class BattlesController : ControllerBase
    {
        private readonly ISyncReadRepository<BattleModel> repository;
        private readonly IIdentifiedUser user;

        public BattlesController(ISyncReadRepository<BattleModel> repository, IIdentifiedUser user)
        {
            this.repository = repository;
            this.user = user;
        }

        [HttpGet("me/current")]
        public async Task<IActionResult> GetCurrentBattle()
        {
            var trainerId = user.Id.Value.ToString();
            var battleOrNothing = (await repository.Find(b => b.AttackerId == trainerId || b.DefenderId == trainerId)).TryFirst(b => !b.EndedAt.HasValue);
            if (battleOrNothing.HasNoValue)
            {
                return NotFound();
            }

            return Ok(battleOrNothing.Value);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GeMyBattles()
        {
            var trainerId = user.Id.Value.ToString();
            var battles = await repository.Find(b => b.AttackerId == trainerId || b.DefenderId == trainerId);
            return Ok(battles.ToList());
        }
    }
}