using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pokens.Pokedex.Business;
using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Office.Pages.Pokemons
{
    public class CreatePokemonModel : PageModel
    {
        private readonly IAbilityService abilityService;
        private readonly IPokemonService pokemonService;

        public CreatePokemonModel(IAbilityService abilityService, IPokemonService pokemonService)
        {
            this.abilityService = abilityService;
            this.pokemonService = pokemonService;
        }

        [BindProperty]
        public CreatePokemonViewModel Pokemon { get; set; }

        public IEnumerable<SelectListItem> Abilities { get; set; }

        public void OnGet()
        {
            this.Abilities = this.abilityService.GetAll().Select(a => new SelectListItem(a.Name, a.Id));
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
            this.pokemonService.Create(Pokemon.Name, stats, Pokemon.Abilities);

            return RedirectToPage("/Pokemons/All");
        }
    }

    public class CreatePokemonViewModel
    {
        public string Name { get; set; }

        public int Health { get; set; }

        public int Defense { get; set; }

        public float DodgeChance { get; set; }

        public int AttackPower { get; set; }

        public float CriticalStrikeChance { get; set; }

        public ICollection<string> Abilities { get; set; }
    }
}