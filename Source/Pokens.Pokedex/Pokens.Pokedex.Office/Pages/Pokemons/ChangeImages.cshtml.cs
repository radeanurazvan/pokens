using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;
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

        public async Task OnGet()
        {
            var id = HttpContext.Request.Query["id"];
            this.PokemonChosen = (await this.pokemonService.GetAll()).FirstOrDefault(p => p.Id == id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = HttpContext.Request.Query["id"];

            var imageId = HttpContext.Request.Query["action"];
            if(imageId.Count != 0)
            {
                await this.pokemonService.DeleteImage(id, imageId);
                return RedirectToPage("/Pokemons/All");
            }
            var files = HttpContext.Request.Form.Files;
            if (files.Count != 0)
                await CreateImage(id, files);
            return RedirectToPage("/Pokemons/All");
        }

        private async Task CreateImage(StringValues id, Microsoft.AspNetCore.Http.IFormFileCollection files)
        {
            var changePokemonImageModel = new ChangePokemonImageModel();
            changePokemonImageModel.Id = id;
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
        }
        public class ChangePokemonImageModel
        {
            public string Id { get; set; }

            public byte[] Content { get; set; }

            public string ImageName { get; set; }
        }
    }
}