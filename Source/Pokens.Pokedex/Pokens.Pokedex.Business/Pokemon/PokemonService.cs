﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Pokedex.Business
{
    internal sealed class PokemonService : IPokemonService
    {
        private readonly IPokedexRepository repository;
        private readonly IMessageBus bus;

        public PokemonService(IPokedexRepository repository, IMessageBus bus)
        {
            this.repository = repository;
            this.bus = bus;
        }

        public IEnumerable<PokemonModel> GetAll()
        {
            return this.repository.GetAll<Pokemon>().Select(p => new PokemonModel(p));
        }

        public Task Create(string name, Stats stats, IEnumerable<string> abilitiesIds)
        {
            var abilities = repository.Find<Ability>(a => abilitiesIds.Contains(a.Id));
            var pokemon = new Pokemon
            {
                Name = name,
                Stats = stats,
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
    }
}