using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokens.Battles.Read.Domain;
using Pomelo.Kernel.DataSynchronization;

namespace Pokens.Battles.Read.WebApi
{
    [Route("api/v1/arenas")]
    public sealed class ArenasController : ControllerBase
    {
        private readonly ISyncReadRepository<ArenaModel> readRepository;

        public ArenasController(ISyncReadRepository<ArenaModel> readRepository)
        {
            this.readRepository = readRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await readRepository.GetAll());
        }
    }
}