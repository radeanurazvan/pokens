using System.Collections.Generic;
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

        public void OnGet()
        {
            this.Abilities = this.service.GetAll();
        }
    }
}