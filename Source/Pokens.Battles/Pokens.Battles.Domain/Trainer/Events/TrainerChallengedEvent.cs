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

        public TrainerChallengedEvent(Guid trainerId, Guid pokemonId, Guid challengedPokemonId)
            : this()
        {
            TrainerId = trainerId;
            PokemonId = pokemonId;
            ChallengedPokemonId = challengedPokemonId;
            ChallengedAt = TimeProvider.Instance().UtcNow;
        }

        public Guid TrainerId { get; private set; }

        public Guid PokemonId { get; private set; }

        public Guid ChallengedPokemonId { get; private set; }

        public DateTime ChallengedAt { get; private set; }
    }
}