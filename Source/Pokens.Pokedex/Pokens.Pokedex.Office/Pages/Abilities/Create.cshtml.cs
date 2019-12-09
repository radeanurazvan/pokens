using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokens.Pokedex.Business;

namespace Pokens.Pokedex.Office.Pages.Abilities
{
    public class CreateModel : PageModel
    {
        private readonly IAbilityService service;

        public CreateModel(IAbilityService service)
        {
            this.service = service;
        }

        [BindProperty]
        public CreateAbilityViewModel Ability { get; set; } = new CreateAbilityViewModel();

        public async Task<IActionResult> OnPost()
        {
            await this.service.Create(Ability.Name, Ability.Description, Ability.Damage, Ability.RequiredLevel, Ability.Cooldown);
            return RedirectToPage("/Abilities/All");
        }
    }

    public sealed class CreateAbilityViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int RequiredLevel { get; set; }

        public int Damage { get; set; }

        public int Cooldown { get; set; }
    }
}