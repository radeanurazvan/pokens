using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public class TrainerHasBeenChallengedEvent : IDomainEvent
    {
        private TrainerHasBeenChallengedEvent()
        {
        }

        public TrainerHasBeenChallengedEvent(Trainer challenger, Guid challengeId, Pokemon challengerPokemon, Pokemon challengedPokemon)
            : this()
        {
            TrainerId = challengedPokemon.TrainerId;
            PokemonId = challengedPokemon.Id;
            Pokemon = challengedPokemon.Name;
            ChallengeId = challengeId;
            ArenaId = challenger.Enrollment.Value;
            ChallengerId = challenger.Id;
            ChallengerName = challenger.Name;
            ChallengerPokemonId = challengerPokemon.Id;
            ChallengerPokemonName = challengerPokemon.Name;
            ChallengedAt = TimeProvider.Instance().UtcNow;
        }

        public Guid TrainerId { get; private set; }

        public Guid ChallengerId { get; private set; }

        public string ChallengerName { get; private set; }
        
        public Guid ChallengeId { get; private set; }

        public Guid ArenaId { get; private set; }

        public Guid ChallengerPokemonId { get; private set; }

        public string ChallengerPokemonName { get; private set; }

        public Guid PokemonId { get; private set; }

        public string Pokemon { get; private set; }

        public DateTime ChallengedAt { get; private set; }
    }
}