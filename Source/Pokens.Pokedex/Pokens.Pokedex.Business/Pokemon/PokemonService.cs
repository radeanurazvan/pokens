﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Pokedex.Business
{
    internal sealed class PokemonService : IPokemonService
    {
        private readonly ICollectionRepository repository;
        private readonly IMessageBus bus;

        public PokemonService(ICollectionRepository repository, IMessageBus bus)
        {
            this.repository = repository;
            this.bus = bus;
        }

        public IEnumerable<PokemonModel> GetAll()
        {
            return this.repository.GetAll<Pokemon>().Select(p => new PokemonModel(p));
        }

        public IEnumerable<StarterPokemonModel> GetStarters()
        {
            return this.repository.GetAll<Pokemon>().Where(p => p.IsStarter).Select(p => new StarterPokemonModel(p));
        }

        public Task Create(string name, Stats stats, IEnumerable<string> abilitiesIds)
        {
            var abilities = repository.Find<Ability>(a => abilitiesIds.Contains(a.Id));
            var pokemon = new Pokemon
            {
                Name = name,
                Stats = stats,
                IsStarter = false,
                Abilities = abilities.ToList()
            };

            this.repository.Add(pokemon);
            return this.bus.Publish(new PokemonCreated(pokemon));
        }

        public Task ChangeStats(string pokemonId, Stats newStats)
        {
            var pokemonOrNothing = this.repository.FindOne<Pokemon>(p => p.Id == pokemonId);
            if (pokemonOrNothing.HasNoValue)
            {
                return Task.CompletedTask;
            }

            var pokemon = pokemonOrNothing.Value;
            pokemon.Stats = newStats;

            this.repository.Update(pokemon);
            return this.bus.Publish(new PokemonStatsChanged(pokemon));
        }

        public Task ChangeAbilities(string pokemonId, IEnumerable<string> abilitiesIds)
        {

            var pokemonOrNothing = this.repository.FindOne<Pokemon>(p => p.Id == pokemonId);
            if (pokemonOrNothing.HasNoValue)
            {
                return Task.CompletedTask;
            }

            var pokemon = pokemonOrNothing.Value;
            pokemon.Abilities = this.repository.Find<Ability>(a => abilitiesIds.Contains(a.Id)).ToList();

            this.repository.Update(pokemon);
            return this.bus.Publish(new PokemonAbilitiesChanged(pokemon));
        }

        public Task ChangeStarter(string pokemonId)
        {
            var pokemonOrNothing = this.repository.FindOne<Pokemon>(p => p.Id == pokemonId);
            if (pokemonOrNothing.HasNoValue)
            {
                return Task.CompletedTask;
            }

            var pokemon = pokemonOrNothing.Value;
            pokemon.IsStarter = ! pokemon.IsStarter;

            this.repository.Update(pokemon);
            return this.bus.Publish(new PokemonStarterChanged(pokemon));
        }
        public Task ChangeImages(string pokemonId, byte[] contentImage, string imageName)
        {
            var pokemonOrNothing = this.repository.FindOne<Pokemon>(p => p.Id == pokemonId);
            if (pokemonOrNothing.HasNoValue)
            {
                return Task.CompletedTask;
            }
            var pokemon = pokemonOrNothing.Value;

            var img = new Image(imageName, contentImage);
            pokemon.Images.Add(img);

            this.repository.Update(pokemon);
            return this.bus.Publish(new PokemonImagesChanged(pokemon));
        }

        public Task DeleteImage(string pokemonId, string imageId)
        {
            var pokemonOrNothing = this.repository.FindOne<Pokemon>(p => p.Id == pokemonId);
            if (pokemonOrNothing.HasNoValue)
            {
                return Task.CompletedTask;
            }
            var pokemon = pokemonOrNothing.Value;

            var image = pokemon.Images.FirstOrDefault(i => i.Id == imageId);

            if (image == null)
            {
                return Task.CompletedTask;
            }
            pokemon.Images.Remove(image);
            this.repository.Update(pokemon);
            return this.bus.Publish(new PokemonImagesChanged(pokemon));
        }
    }
}