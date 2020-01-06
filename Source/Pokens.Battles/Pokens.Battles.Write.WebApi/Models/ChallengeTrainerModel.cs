using System;

namespace Pokens.Battles.Write.WebApi.Models
{
    public sealed class ChallengeTrainerModel
    {
        public Guid ChallengerPokemonId { get; set; }

        public Guid ChallengedPokemonId { get; set; }
    }
}