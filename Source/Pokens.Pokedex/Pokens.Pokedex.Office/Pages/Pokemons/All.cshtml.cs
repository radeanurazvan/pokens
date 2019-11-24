﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokens.Pokedex.Business;

namespace Pokens.Pokedex.Office.Pages.Pokemons
{
    public class AllPokemonsModel : PageModel
    {
        private readonly IPokemonService service;

        public AllPokemonsModel(IPokemonService service)
        {
            this.service = service;
        }

        public IEnumerable<PokemonModel> Pokemons { get; private set; } = new List<PokemonModel>();

        public void OnGet()
        {
            this.Pokemons = this.service.GetAll();
        }
    }
}