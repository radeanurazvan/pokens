using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerChallengedEvent : IDomainEvent
    {
        private TrainerChallengedEvent()
        {
        }

        public TrainerChallengedEvent(Trainer trainer, Guid challengeId, Pokemon pokemon, Pokemon challengedPokemon)
            : this()
        {
            TrainerId = trainer.Id;
            TrainerName = trainer.Name;
            ChallengeId = challengeId;
            ArenaId = trainer.Enrollment.Value;
            PokemonId = pokemon.Id;
            PokemonName = pokemon.Name;
            ChallengedPokemonId = challengedPokemon.Id;
            ChallengedPokemonName = challengedPokemon.Name;
            ChallengedAt = TimeProvider.Instance().UtcNow;
        }

        public Guid TrainerId { get; private set; }

        public string TrainerName { get; private set; }

        public Guid ChallengeId { get; private set; }

        public Guid ArenaId { get; private set; }

        public Guid PokemonId { get; private set; }

        public string PokemonName { get; private set; }

        public Guid ChallengedPokemonId { get; private set; }

        public string ChallengedPokemonName { get; private set; }

        public DateTime ChallengedAt { get; private set; }
    }
}