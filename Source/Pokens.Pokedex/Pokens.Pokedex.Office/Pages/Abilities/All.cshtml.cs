using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokens.Pokedex.Business;
using Microsoft.Extensions.Primitives;
using System.Net.Http.Headers;
using System;
using System.IO;

namespace Pokens.Pokedex.Office.Pages.Abilities
{
    public class AllAbilitiesModel : PageModel
    {
        private readonly IAbilityService service;

        public AllAbilitiesModel(IAbilityService service)
        {
            this.service = service;
        }

        public IEnumerable<AbilityModel> Abilities { get; set; }

        public async Task OnGet()
        {
            this.Abilities = await this.service.GetAll();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = HttpContext.Request.Query["action"];
            var files = HttpContext.Request.Form.Files;
            if (files.Count != 0)
                await CreateImage(id, files);
            return RedirectToPage("/Abilities/All");
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
                        await this.service.AddImage(changePokemonImageModel.Id,
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