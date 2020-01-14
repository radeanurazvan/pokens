using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokens.Battles.Read.Domain;
using Pomelo.Kernel.DataSynchronization;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Read.WebApi
{
    [Authorize]
    [Route("api/v1/arenas")]
    public sealed class ArenasController : ControllerBase
    {
        private readonly ISyncReadRepository<ArenaModel> readRepository;
        private readonly IIdentifiedUser user;

        public ArenasController(ISyncReadRepository<ArenaModel> readRepository, IIdentifiedUser user)
        {
            this.readRepository = readRepository;
            this.user = user;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await readRepository.GetAll());
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyArena()
        {
            var arena = await readRepository.Find(a => a.HasTrainer(this.user.Id.Value.ToString()));
            if (arena == null)
            {
                return NotFound();
            }

            return Ok(arena);
        }
    }
}