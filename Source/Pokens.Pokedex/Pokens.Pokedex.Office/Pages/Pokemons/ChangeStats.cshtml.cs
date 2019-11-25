using System;
using System.Collections.Generic;
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
        public string Id { get; set; }
        public PokemonModel PokemonChosen { get; set; }

        [BindProperty]
        public CreatePokemonViewModel Pokemon { get; set; }

        public void OnGet()
        {
            Id = HttpContext.Request.Query["id"];
            this.PokemonChosen = this.pokemonService.GetAll().Where(p => p.Id == this.Id).FirstOrDefault();
        }
        public IActionResult OnPost()
        {
            var stats = new Stats
            {
                Health = Pokemon.Health,
                AttackPower = Pokemon.AttackPower,
                CriticalStrikeChance = Pokemon.CriticalStrikeChance,
                Defense = Pokemon.Defense,
                DodgeChance = Pokemon.DodgeChance
            };
            this.pokemonService.ChangeStats(Id, stats);

            return RedirectToPage("/Pokemons/All");
        }
    }
}