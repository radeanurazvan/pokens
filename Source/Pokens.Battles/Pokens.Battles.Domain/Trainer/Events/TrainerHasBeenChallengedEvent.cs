﻿using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

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
            ChallengedAt = TimeProvider.Instance().UtcNow;
        }

        public Guid ChallengerId { get; private set; }

        public Guid ChallengerPokemonId { get; private set; }
        
        public Guid PokemonId { get; private set; }

        public DateTime ChallengedAt { get; private set; }
    }
}