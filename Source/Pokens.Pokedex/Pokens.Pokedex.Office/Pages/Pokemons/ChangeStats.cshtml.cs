using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokens.Pokedex.Business;
using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Office.Pages.Pokemons
{
    public class ChangeStatsModel : PageModel
    {
        private readonly IPokemonService pokemonService;
        public ChangeStatsModel(IPokemonService pokemonService)
        {
            this.pokemonService = pokemonService;
        }
        public PokemonModel PokemonChosen { get; set; }

        [BindProperty]
        public CreatePokemonViewModel Pokemon { get; set; }

        public async Task OnGet()
        {
            var id = HttpContext.Request.Query["id"];
            this.PokemonChosen = (await this.pokemonService.GetAll()).FirstOrDefault(p => p.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            var id = HttpContext.Request.Query["id"];
            var stats = new Stats
            {
                Health = Pokemon.Health,
                AttackPower = Pokemon.AttackPower,
                CriticalStrikeChance = Pokemon.CriticalStrikeChance,
                Defense = Pokemon.Defense,
                DodgeChance = Pokemon.DodgeChance
            };
            await this.pokemonService.ChangeStats(id, stats);

            return RedirectToPage("/Pokemons/All");
        }
    }
}