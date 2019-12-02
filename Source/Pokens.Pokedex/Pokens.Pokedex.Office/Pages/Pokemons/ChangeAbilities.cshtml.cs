using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pokens.Pokedex.Business;

namespace Pokens.Pokedex.Office.Pages.Pokemons
{
    public class ChangeAbilitiesModel : PageModel
    {
        private readonly IAbilityService abilityService;
        private readonly IPokemonService pokemonService;

        public ChangeAbilitiesModel(IAbilityService abilityService, IPokemonService pokemonService)
        {
            this.abilityService = abilityService;
            this.pokemonService = pokemonService;
        }

        [BindProperty]
        public ChangePokemonAbilitiesModel Pokemon { get; set; }
        public PokemonModel PokemonChosen { get; set; }

        public IEnumerable<SelectListItem> Abilities { get; set; }
        public void OnGet()
        {
            var id = HttpContext.Request.Query["id"];
            this.PokemonChosen = this.pokemonService.GetAll().Where(p => p.Id == id).FirstOrDefault();

            this.Abilities = this.abilityService.GetAll().Select(a => new SelectListItem(a.Name, a.Id));
        }
        public IActionResult OnPost()
        {
            var id = HttpContext.Request.Query["id"];
            this.pokemonService.ChangeAbilities(id, Pokemon.Abilities);
            return RedirectToPage("/Pokemons/All");
        }

    }

    public class ChangePokemonAbilitiesModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<string> Abilities { get; set; }
    }
}