using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class Trainer : AggregateRoot
    {
        private readonly ICollection<Pokemon> pokemons = new List<Pokemon>();
        private readonly ICollection<Challenge> challenges = new List<Challenge>();

        private Trainer()
        {
        }

        private Trainer(Guid id , string name)
            : this()
        {
            ReactToDomainEvent(new TrainerRegisteredEvent(id, name));
        }

        public static Trainer Register(Guid id, string name)
        {
            return new Trainer(id, name);
        }

        public string Name { get; private set; }

        public Maybe<Guid> Enrollment { get; private set; } = Maybe<Guid>.None;

        public bool IsEnrolled => Enrollment.HasValue;

        public bool IsEnrolledIn(Arena arena) => IsEnrolled && Enrollment == arena.Id;

        public IEnumerable<Challenge> Challenges => this.challenges;

        public void Catch(Pokemon pokemon)
        {
            ReactToDomainEvent(new TrainerCaughtPokemonEvent(pokemon));
        }

        internal Result EnrollIn(Arena arena)
        {
            return Result.FailureIf(Enrollment.HasValue, Messages.TrainerAlreadyEnrolled)
                .Tap(() => ReactToDomainEvent(new TrainerEnrolledEvent(arena.Id)));
        }

        internal Result LeaveArena()
        {
            return Result.SuccessIf(Enrollment.HasValue, Messages.TrainerIsNotEnrolled)
                .Tap(() => ReactToDomainEvent(new TrainerLeftArenaEvent()));
        }

        internal Result Challenge(Trainer challenged)
        {
            return Result.FailureIf(challenges.Any(c => c.HasParticipants(this, challenged)), Messages.TrainerAlreadyChallenged)
                .Ensure(() => this != challenged, Messages.CannotChallengeSelf)
                .Tap(() => ReactToDomainEvent(new TrainerChallengedEvent(challenged.Id)))
                .Tap(() => challenged.ReceiveChallengeFrom(this));
        }

        internal int EnrollmentLevel => pokemons.Select(p => p.Level).Max();

        private void ReceiveChallengeFrom(Trainer challenger) => ReactToDomainEvent(new TrainerHasBeenChallengedEvent(challenger.Id));

        private void When(TrainerRegisteredEvent @event)
        {
            Id = @event.Id;
            Name = @event.Name;
        }

        private void When(TrainerCaughtPokemonEvent @event)
        {
            pokemons.Add(@event.Pokemon);
        }

        private void When(TrainerEnrolledEvent @event)
        {
            Enrollment = @event.ArenaId;
        }

        private void When(TrainerLeftArenaEvent @event)
        {
            Enrollment = Maybe<Guid>.None;
        }

        private void When(TrainerChallengedEvent @event)
        {
            var challenge = Domain.Challenge.For(@event.TrainerId)
                .By(this.Id)
                .On(Enrollment.Unwrap());
            challenges.Add(challenge);
        }

        private void When(TrainerHasBeenChallengedEvent @event)
        {
            var challenge = Domain.Challenge.For(this.Id)
                .By(@event.ChallengerId)
                .On(Enrollment.Unwrap());
            challenges.Add(challenge);
        }
    }
}