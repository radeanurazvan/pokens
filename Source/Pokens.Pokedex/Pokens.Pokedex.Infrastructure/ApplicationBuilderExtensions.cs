using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pokens.Pokedex.Business;
using Pokens.Pokedex.Domain;
using Pokens.Pokedex.Infrastructure.Seed;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokens.Pokedex.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDefaultPokemons(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<ICollectionRepository>();
                var pokemonService = scope.ServiceProvider.GetService<IPokemonService>();
                if (!repository.GetAll<Ability>().GetAwaiter().GetResult().Any())
                {
                    DefaultAbilities.All.ForEach(a => repository.Add(a).GetAwaiter().GetResult());
                }

                if (!repository.GetAll<Pokemon>().GetAwaiter().GetResult().Any())
                {
                    DefaultPokemons.All.ForEach(p =>
                    {
                        var pokemonId = pokemonService.Create(p.Name, p.Stats, p.Abilities.Select(a => a.Id), p.CatchRate).GetAwaiter().GetResult();
                        pokemonService.AddImage(pokemonId, p.Images.First().ContentImage, p.Images.First().ImageName).GetAwaiter().GetResult();
                        if (p.IsStarter)
                        {
                            pokemonService.ChangeStarter(pokemonId).GetAwaiter().GetResult();
                        }
                    });
                }
            }

            return app;
        }
    }
}