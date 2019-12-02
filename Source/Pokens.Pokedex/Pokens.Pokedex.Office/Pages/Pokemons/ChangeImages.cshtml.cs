using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokens.Pokedex.Business;

namespace Pokens.Pokedex.Office.Pages.Pokemons
{
    public class ChangeImagesModel : PageModel
    {
        private readonly IPokemonService pokemonService;
        public ChangeImagesModel(IPokemonService pokemonService)
        {
            this.pokemonService = pokemonService;
        }
        public PokemonModel PokemonChosen { get; set; }

        [BindProperty]
        public CreatePokemonViewModel Pokemon { get; set; }

        [BindProperty]
        public ChangePokemonImageModel PokemonImage { get; set; }

        public void OnGet()
        {
            var id = HttpContext.Request.Query["id"];
            this.PokemonChosen = this.pokemonService.GetAll().Where(p => p.Id == id).FirstOrDefault();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var changePokemonImageModel = new ChangePokemonImageModel();
            changePokemonImageModel.Id = HttpContext.Request.Query["id"];
            var files = HttpContext.Request.Form.Files;
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {

                    var file = Image;
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse
                            (file.ContentDisposition).FileName.Trim('"');

                        using (var reader = new BinaryReader(file.OpenReadStream()))
                        {
                            file.OpenReadStream();
                            changePokemonImageModel.Content = reader.ReadBytes(Convert.ToInt32(file.Length));
                            changePokemonImageModel.ImageName = file.FileName;
                        }
                        await this.pokemonService.ChangeImages(changePokemonImageModel.Id,
                                changePokemonImageModel.Content, changePokemonImageModel.ImageName);
                    }
                }
            }

            return RedirectToPage("/Pokemons/All");
        }

        public class ChangePokemonImageModel
        {
            public string Id { get; set; }

            public byte[] Content { get; set; }

            public string ImageName { get; set; }
        }
    }
}