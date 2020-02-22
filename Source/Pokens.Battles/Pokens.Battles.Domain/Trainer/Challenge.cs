using System;
using System.Collections.Generic;
using System.Linq;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class Challenge : Entity, ISteadyChallenge, IReadyChallenge, IUnscheduledChallenge
    {
        public static readonly TimeSpan TimeToLive = TimeSpan.FromHours(6);

        private Challenge()
        {
        }

        public static ISteadyChallenge For(Guid challengeId, Guid challengedId, Guid challengedPokemonId)
        {
            return new Challenge
            {
                Id = challengeId,
                ChallengedId = challengedId,
                ChallengedPokemonId = challengedPokemonId,
                ExpiresAt = TimeProvider.Instance().UtcNow.Add(TimeToLive),
                Status = ChallengeStatus.Pending
            };
        }

        public IReadyChallenge By(Guid challengerId, Guid challengerPokemonId)
        {
            ChallengerId = challengerId;
            ChallengerPokemonId = challengerPokemonId;
            return this;
        }

        public IUnscheduledChallenge On(Guid arenaId)
        {
            ArenaId = arenaId;
            return this;
        }

        public Guid ChallengerId { get; private set; }
        
        public Guid ChallengerPokemonId { get; private set; }

        public Guid ChallengedId { get; private set; }
        
        public Guid ChallengedPokemonId { get; private set; }

        public Guid ArenaId { get; private set; }

        public DateTime ExpiresAt { get; private set; }

        public bool HasParticipants(Trainer first, Trainer second) => HasParticipants(first.Id, second.Id);

        public bool HasParticipants(Guid first, Guid second)
        {
            var givenParticipants = new List<Guid> { first, second };
            givenParticipants.Sort();
            var realParticipants = new List<Guid> { ChallengedId, ChallengerId };
            realParticipants.Sort();

            return givenParticipants.SequenceEqual(realParticipants);
        }

        public Challenge At(DateTime time)
        {
            ExpiresAt = time.Add(TimeToLive);
            return this;
        }

        public ChallengeStatus Status;

        internal void MarkAsAccepted()
        {
            Status = ChallengeStatus.Accepted;
        }

        internal void MarkAsRejected()
        {
            Status = ChallengeStatus.Rejected;
        }

        internal void MarkAsHonored()
        {
            Status = ChallengeStatus.Honored;
        }

        internal bool IsPending => Status == ChallengeStatus.Pending && !IsExpired;

        internal bool IsAccepted => Status == ChallengeStatus.Accepted;

        internal bool IsExpired => ExpiresAt < TimeProvider.Instance().UtcNow;

        internal bool IsNotExpired => !IsExpired;
    }

    public enum ChallengeStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        Honored = 4
    }

    public interface ISteadyChallenge
    {
        IReadyChallenge By(Guid challengerId, Guid challengerPokemonId);
    }

    public interface IReadyChallenge
    {
        IUnscheduledChallenge On(Guid arenaId);
    }

    public interface IUnscheduledChallenge
    {
        Challenge At(DateTime time);
    }
}