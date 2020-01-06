using System;
using System.Collections.Generic;
using System.Linq;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class Challenge : Entity, ISteadyChallenge, IReadyChallenge
    {
        private Challenge()
        {
        }

        public static ISteadyChallenge For(Guid challengedId, Guid challengedPokemonId)
        {
            return new Challenge {ChallengedId = challengedId, ChallengedPokemonId = challengedPokemonId};
        }

        public IReadyChallenge By(Guid challengerId, Guid challengerPokemonId)
        {
            ChallengerId = challengerId;
            ChallengerPokemonId = challengerPokemonId;
            return this;
        }

        public Challenge On(Guid arenaId)
        {
            ArenaId = arenaId;
            return this;
        }

        public Guid ChallengerId { get; private set; }
        
        public Guid ChallengerPokemonId { get; private set; }

        public Guid ChallengedId { get; private set; }
        
        public Guid ChallengedPokemonId { get; private set; }

        public Guid ArenaId { get; private set; }

        public bool HasParticipants(Trainer first, Trainer second)
        {
            var givenParticipants = new List<Guid>{ first.Id, second.Id };
            givenParticipants.Sort();
            var realParticipants = new List<Guid>{ ChallengedId, ChallengerId };
            realParticipants.Sort();

            return givenParticipants.SequenceEqual(realParticipants);
        }
    }

    public interface ISteadyChallenge
    {
        IReadyChallenge By(Guid challengerId, Guid challengerPokemonId);
    }

    public interface IReadyChallenge
    {
        Challenge On(Guid arenaId);
    }
}