using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokens.Pokedex.Business;

namespace Pokens.Pokedex.Office.Pages.Abilities
{
    public class AllAbilitiesModel : PageModel
    {
        private readonly IAbilityService service;

        public AllAbilitiesModel(IAbilityService service)
        {
            this.service = service;
        }

        public IEnumerable<AbilityModel> Abilities { get; set; }

        public async Task OnGet()
        {
            this.Abilities = await this.service.GetAll();
        }
    }
}