using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokens.Pokedex.Business;

namespace Pokens.Pokedex.Office.Pages.Pokemons
{
    public class ChangeStarterModel : PageModel
    {
        private readonly IPokemonService pokemonService;
        public ChangeStarterModel(IPokemonService pokemonService)
        {
            this.pokemonService = pokemonService;
        }
        public IActionResult OnGet()
        {
            var id = HttpContext.Request.Query["id"];
            this.pokemonService.ChangeStarter(id);
            return RedirectToPage("/Pokemons/All");
        }
    }
}