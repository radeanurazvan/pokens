using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokens.Pokedex.Business;

namespace Pokens.Pokedex.Office.Pages.Pokemons
{
    public class DeleteImageModel : PageModel
    {
        private readonly IPokemonService pokemonService;
        public DeleteImageModel(IPokemonService pokemonService)
        {
            this.pokemonService = pokemonService;
        }
        public IActionResult OnGet()
        {
            var id = HttpContext.Request.Query["id"];
            var imageId = HttpContext.Request.Query["imageId"];
            this.pokemonService.DeleteImage(id, imageId);
            return RedirectToPage("/Pokemons/All");
        }
    }
}