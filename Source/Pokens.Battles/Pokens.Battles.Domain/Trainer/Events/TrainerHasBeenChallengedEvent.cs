using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public class TrainerHasBeenChallengedEvent : IDomainEvent
    {
        private TrainerHasBeenChallengedEvent()
        {
        }

        public TrainerHasBeenChallengedEvent(Guid pokemonId, Guid challengerId, Guid challengerPokemonId)
            : this()
        {
            PokemonId = pokemonId;
            ChallengerId = challengerId;
            ChallengerPokemonId = challengerPokemonId;
            ChallengedAt = DateTimeProvider.Instance().UtcNow;
        }

        public Guid ChallengerId { get; private set; }

        public Guid ChallengerPokemonId { get; private set; }
        
        public Guid PokemonId { get; private set; }

        public DateTime ChallengedAt { get; private set; }
    }
}