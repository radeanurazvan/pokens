using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Domain;

namespace Pokens.Pokedex.Business
{
    internal sealed class PokemonService : IPokemonService
    {
        private readonly ICollectionRepository repository;

        public PokemonService(ICollectionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<PokemonModel>> GetAll()
        {
            return ( await this.repository.GetAll<Pokemon>()).Select(p => new PokemonModel(p));
        }

        public async Task<IEnumerable<StarterPokemonModel>> GetStarters()
        {
            return (await this.repository.Find<Pokemon>(p => p.IsStarter)).Select(p => new StarterPokemonModel(p));
        }

        public async Task<IEnumerable<PokemonModel>> GetPokemonRoulette()
        {
            var rouletteResult = PokemonRoulette.SpinRoulette();
            return (await repository.GetAll<Pokemon>())
                .OrderBy(p => Guid.NewGuid())
                .Take(rouletteResult)
                .Select(p => new PokemonModel(p));
        }

        public async Task<string> Create(string name, Stats stats, IEnumerable<string> abilitiesIds, double catchRate)
        {
            var abilities = await repository.Find<Ability>(a => abilitiesIds.Contains(a.Id));
            var pokemon = new Pokemon(name, stats, false, catchRate, abilities.ToList());

            await this.repository.Add(pokemon);
            await this.repository.Commit();
            return pokemon.Id;
        }

        public async Task ChangeStats(string pokemonId, Stats newStats)
        {
            var pokemonOrNothing = await this.repository.FindOne<Pokemon>(p => p.Id == pokemonId);
            if (pokemonOrNothing.HasNoValue)
            {
                return;
            }

            var pokemon = pokemonOrNothing.Value;
            pokemon.ChangeStats(newStats);

            await this.repository.Update(pokemon);
            await this.repository.Commit();
        }

        public async Task ChangeAbilities(string pokemonId, IEnumerable<string> abilitiesIds)
        {
            var pokemonOrNothing = await this.repository.FindOne<Pokemon>(p => p.Id == pokemonId);
            if (pokemonOrNothing.HasNoValue)
            {
                return;
            }

            var pokemon = pokemonOrNothing.Value;
            pokemon.ChangeAbilities((await this.repository.Find<Ability>(a => abilitiesIds.Contains(a.Id))).ToList());


            await this.repository.Update(pokemon);
            await this.repository.Commit();
        }

        public async Task ChangeStarter(string pokemonId)
        {
            var pokemonOrNothing = await this.repository.FindOne<Pokemon>(p => p.Id == pokemonId);
            if (pokemonOrNothing.HasNoValue)
            {
                return;
            }

            var pokemon = pokemonOrNothing.Value;
            pokemon.ToggleStarter();

            await this.repository.Update(pokemon);
            await this.repository.Commit();
        }
        public async Task AddImage(string pokemonId, byte[] contentImage, string imageName)
        {
            var pokemonOrNothing = await this.repository.FindOne<Pokemon>(p => p.Id == pokemonId);
            if (pokemonOrNothing.HasNoValue)
            {
                return;
            }
            var pokemon = pokemonOrNothing.Value;
            var img = new Image(imageName, contentImage);
            pokemon.AddImage(img);

            await this.repository.Update(pokemon);
            await this.repository.Commit();
        }

        public async Task DeleteImage(string pokemonId, string imageId)
        {
            var pokemonOrNothing = await this.repository.FindOne<Pokemon>(p => p.Id == pokemonId);
            if (pokemonOrNothing.HasNoValue)
            {
                return;
            }
            var pokemon = pokemonOrNothing.Value;

            var image = pokemon.Images.FirstOrDefault(i => i.Id == imageId);
            if (image == null)
            {
                return;
            }
            
            pokemon.DeleteImage(image);
            await this.repository.Update(pokemon);
            await this.repository.Commit();
        }
    }   
}