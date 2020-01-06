using System;
using System.Linq;

namespace Pokens.Battles.Domain.Tests.Extensions
{
    public static class TrainerExtensions
    {
        public static Guid FirstPokemonId(this Trainer trainer) => trainer.Pokemons.First().Id;
    }
}