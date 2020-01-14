using System.Linq;
using FluentAssertions;
using Pokens.Battles.Domain.Tests.Extensions;
using Pokens.Battles.Resources;
using Xunit;

namespace Pokens.Battles.Domain.Tests
{
    public sealed class ArenaTests
    {
        [Fact]
        public void Given_Enroll_When_TrainerIsAlreadyEnrolled_Then_ShouldFail()
        {
            // Arrange
            var sut = ArenaFactory.WithoutRequirement();
            var trainer = TrainerFactory.EnrolledIn(sut);

            // Act
            var result = sut.Enroll(trainer);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.TrainerAlreadyEnrolled);
            sut.Events.Should().NotContain(e => e is ArenaEnrolledTrainerEvent);
            trainer.Events.Should().NotContain(e => e is TrainerEnrolledEvent);
        }

        [Fact]
        public void Given_Enroll_When_TrainerDoesNotMeetRequiredLevel_Then_ShouldFail()
        {
            // Arrange
            var trainer = TrainerFactory.WithLevel(1);
            var sut = ArenaFactory.RequiringLevel(2);

            // Act
            var result = sut.Enroll(trainer);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.TrainerDoesNotMeetMinimumLevel);
            sut.Events.Should().NotContain(e => e is ArenaEnrolledTrainerEvent);
            trainer.Events.Should().NotContain(e => e is TrainerEnrolledEvent);
        }

        [Fact]
        public void Given_Enroll_When_TrainerMeetsRequiredLevel_Then_ShouldEnroll()
        {
            // Arrange
            var trainer = TrainerFactory.WithLevel(10);
            var sut = ArenaFactory.RequiringLevel(5);

            // Act
            var result = sut.Enroll(trainer);

            // Assert
            result.IsSuccess.Should().BeTrue();
            sut.Events.Should().ContainSingle(e => e is ArenaEnrolledTrainerEvent);
            sut.Trainers.Should().Contain(t => t.Id == trainer.Id);
            trainer.Events.Should().ContainSingle(e => e is TrainerEnrolledEvent);
        }

        [Fact]
        public void Given_EndEnrollment_When_TrainerIsNotEnrolled_Then_ShouldFail()
        {
            // Arrange
            var trainer = TrainerFactory.Enrolled();
            var sut = ArenaFactory.WithoutRequirement();

            // Act
            var result = sut.EndEnrollmentFor(trainer);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.TrainerIsNotEnrolled);
            sut.Events.Should().NotContain(e => e is ArenaEnrollmentEndedEvent);
            trainer.Events.Should().NotContain(e => e is TrainerLeftArenaEvent);
        }

        [Fact]
        public void Given_EndEnrollment_When_TrainerIsEnrolled_Then_ShouldEnd()
        {
            // Arrange
            var sut = ArenaFactory.WithoutRequirement();
            var trainer = TrainerFactory.EnrolledIn(sut);

            // Act
            var result = sut.EndEnrollmentFor(trainer);

            // Assert
            result.IsSuccess.Should().BeTrue();
            sut.Trainers.Should().NotContain(t => t.Id == trainer.Id);
            sut.Events.Should().ContainSingle(e => e is ArenaEnrollmentEndedEvent);
            trainer.Events.Should().ContainSingle(e => e is TrainerLeftArenaEvent);
        }

        [Fact]
        public void Given_MediateChallenge_When_OneTrainerIsNotEnrolled_Then_ShouldFail()
        {
            // Arrange
            var sut = ArenaFactory.WithoutRequirement();
            var challenger = TrainerFactory.WithLevel(1);
            var challenged = TrainerFactory.EnrolledIn(sut);

            // Act
            var result = sut.MediateChallenge(challenger, challenger.FirstPokemonId(), challenged, challenged.FirstPokemonId());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.TrainerIsNotEnrolled);
            sut.Events.Should().NotContain(e => e is ChallengeOccurredEvent);
            challenger.Events.Should().BeEmpty();
            challenged.Events.Should().BeEmpty();
        }

        [Fact]
        public void Given_MediateChallenge_When_TrainersDoNotHaveSameEnrollment_Then_ShouldFail()
        {
            // Arrange
            var sut = ArenaFactory.WithoutRequirement();
            var challenger = TrainerFactory.Enrolled();
            var challenged = TrainerFactory.EnrolledIn(sut);

            // Act
            var result = sut.MediateChallenge(challenger, challenger.FirstPokemonId(), challenged, challenged.FirstPokemonId());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.TrainersDoNotHaveSameEnrollment);
            sut.Events.Should().NotContain(e => e is ChallengeOccurredEvent);
            challenger.Events.Should().BeEmpty();
            challenged.Events.Should().BeEmpty();
        }

        [Fact]
        public void Given_MediateChallenge_When_TrainersHaveSameEnrollment_Then_ShouldChallenge()
        {
            // Arrange
            var sut = ArenaFactory.WithoutRequirement();
            var challenger = TrainerFactory.EnrolledIn(sut);
            var challenged = TrainerFactory.EnrolledIn(sut);

            // Act
            var result = sut.MediateChallenge(challenger, challenger.FirstPokemonId(), challenged, challenged.FirstPokemonId());

            // Assert
            result.IsSuccess.Should().BeTrue();
            sut.Events.Should().ContainSingle(e => e is ChallengeOccurredEvent);
            challenger.Events.Should().ContainSingle(e => e is TrainerChallengedEvent);
            challenged.Events.Should().ContainSingle(e => e is TrainerHasBeenChallengedEvent);
        }

        [Fact]
        public void Given_MediateChallengeApproval_When_TrainerIsNotEnrolled_Then_ShouldFail()
        {
            // Arrange
            var sut = ArenaFactory.WithoutRequirement();
            var challenger = TrainerFactory.EnrolledIn(sut);
            var challenged = TrainerFactory.ChallengedBy(challenger);

            // Act
            var result = sut.MediateChallengeApproval(challenger, challenged, challenged.Challenges.First().Id);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.ArenaAlreadyLeft);
            sut.Events.Should().NotContain(e => e is ChallengeAcceptedEvent);
            challenged.Events.Should().NotContain(e => e is TrainerAcceptedChallengeEvent);
            challenged.Events.Should().NotContain(e => e is TrainerChallengeAnsweredEvent);
        }

        [Fact]
        public void Given_MediateChallengeApproval_When_TrainerIsNotEnrolledInChallengedArena_Then_ShouldFail()
        {
            // Arrange
            var sut = ArenaFactory.WithoutRequirement();
            var challenger = TrainerFactory.EnrolledIn(ArenaFactory.WithoutRequirement());
            var challenged = TrainerFactory.ChallengedBy(challenger);

            // Act
            var result = sut.MediateChallengeApproval(challenger, challenged, challenged.Challenges.First().Id);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.ArenaAlreadyLeft);
            sut.Events.Should().NotContain(e => e is ChallengeAcceptedEvent);
            challenged.Events.Should().NotContain(e => e is TrainerAcceptedChallengeEvent);
            challenged.Events.Should().NotContain(e => e is TrainerChallengeAnsweredEvent);
        }

        [Fact]
        public void Given_MediateChallengeApproval_When_TrainersAreInChallengedArena_Then_ShouldMediate()
        {
            // Arrange
            var sut = ArenaFactory.WithoutRequirement();
            var challenger = TrainerFactory.EnrolledIn(sut);
            var challenged = TrainerFactory.ChallengedWith(challenger, sut);

            // Act
            var result = sut.MediateChallengeApproval(challenger, challenged, challenged.Challenges.First().Id);

            // Assert
            result.IsSuccess.Should().BeTrue();
            sut.Events.Should().ContainSingle(e => e is ChallengeAcceptedEvent);
        }
    }
}