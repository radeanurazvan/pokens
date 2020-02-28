using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class Arena : AggregateRoot
    {
        private readonly ICollection<TrainerInArena> trainers = new List<TrainerInArena>();
        private readonly ICollection<ChallengeInArena> challenges = new List<ChallengeInArena>();

        private Arena()
        {
        }

        private Arena(string name, int requiredLevel)
            : this()
        {
            ReactToDomainEvent(new ArenaOpenedEvent(Id, name, requiredLevel));
        }

        public static Result<Arena> Open(string name, int requiredLevel)
        {
            var nameResult = name.EnsureValidString(Messages.InvalidName);
            var levelResult = Result.SuccessIf(requiredLevel >= 0, Messages.InvalidArenaLevel);

            return Result.FirstFailureOrSuccess(nameResult, levelResult)
                .Map(() => new Arena(name, requiredLevel));
        }

        public string Name { get; private set; }

        public int RequiredLevel { get; private set; }

        public IEnumerable<TrainerInArena> Trainers => this.trainers;

        public IEnumerable<ChallengeInArena> Challenges => this.challenges; 

        public Result Enroll(Trainer trainer)
        {
            return trainer.EnsureExists(Messages.InvalidTrainer)
                .Ensure(t => !HasEnrollmentFor(t), Messages.TrainerAlreadyEnrolled)
                .Ensure(t => t.EnrollmentLevel >= RequiredLevel, Messages.TrainerDoesNotMeetMinimumLevel)
                .Bind(t => t.EnrollIn(this))
                .Tap(() => ReactToDomainEvent(new ArenaEnrolledTrainerEvent(trainer)));
        }

        public Result EndEnrollmentFor(Trainer trainer)
        {
            return Result.SuccessIf(HasEnrollmentFor(trainer), Messages.TrainerIsNotEnrolled)
                .Bind(trainer.LeaveArena)
                .Tap(() => ReactToDomainEvent(new ArenaEnrollmentEndedEvent(trainer.Id)));
        }

        public Result MediateChallenge(Trainer challenger, Guid challengerPokemonId, Trainer challenged, Guid challengedPokemonId)
        {
            return Result.SuccessIf(challenger.IsEnrolled && challenged.IsEnrolled, Messages.TrainerIsNotEnrolled)
                .Ensure(() => challenger.IsEnrolledIn(this) && challenged.IsEnrolledIn(this), Messages.TrainersDoNotHaveSameEnrollment)
                .Bind(() => challenger.Challenge(challenged, challengerPokemonId, challengedPokemonId))
                .Tap(challengeId => ReactToDomainEvent(new ChallengeOccurredEvent(challengeId, challenger.Id, challenged.Id)));
        }

        public Result MediateChallengeApproval(Trainer challenger, Trainer challenged, Guid challengeId)
        {
            var challengerResult = challenger.EnsureExists(Messages.InvalidTrainer);
            var challengedResult = challenged.EnsureExists(Messages.InvalidTrainer);

            return Result.FirstFailureOrSuccess(challengedResult, challengerResult)
                .Bind(() => challenged.Challenges.FirstOrNothing(c => c.Id == challengeId).ToResult(Messages.ChallengeNotFound))
                .Ensure(c => c.ArenaId == this.Id, Messages.ArenaAlreadyLeft)
                .Ensure(c => challenged.IsEnrolledIn(c.ArenaId), Messages.ArenaAlreadyLeft)
                .Ensure(c => challenger.IsEnrolledIn(c.ArenaId), Messages.ArenaAlreadyLeft)
                .Bind(c => challenged.AcceptChallenge(challenger, c))
                .Tap(() => AddDomainEvent(new ChallengeAcceptedEvent(challengeId)));
        }

        private bool HasEnrollmentFor(Trainer trainer) => trainers.Any(t => t.Id == trainer.Id);

        private void When(ArenaOpenedEvent @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            RequiredLevel = @event.RequiredLevel;
        }

        private void When(ArenaEnrolledTrainerEvent @event)
        {
            trainers.Add(new TrainerInArena(@event.TrainerId, @event.Name, @event.JoinedAt));
        }

        private void When(ArenaEnrollmentEndedEvent @event)
        {
            trainers.FirstOrNothing(t => t.Id == @event.TrainerId)
                .Execute(t => trainers.Remove(t));
        }

        private void When(ChallengeOccurredEvent @event)
        {
            this.challenges.Add(new ChallengeInArena(@event.Id, @event.ChallengerId, @event.ChallengedId));
        }

        private void When(ChallengeAcceptedEvent @event)
        {
        }
    }
}