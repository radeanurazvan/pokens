using System;
using System.Linq;
using FluentAssertions;
using Pokens.Battles.Domain.Tests.Extensions;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;
using Xunit;

namespace Pokens.Battles.Domain.Tests
{
    public class TrainerTests
    {
        [Fact]
        public void Given_EnrollIn_When_TrainerIsAlreadyEnrolled_Then_ShouldFail()
        {
            // Arrange
            var sut = TrainerFactory.Enrolled();
            var arena = ArenaFactory.WithoutRequirement();

            // Act
            var result = sut.EnrollIn(arena);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.TrainerAlreadyEnrolled);
            sut.Events.Should().NotContain(e => e is TrainerEnrolledEvent);
        }

        [Fact]
        public void Given_EnrollIn_When_TrainerIsNotEnrolled_Then_ShouldSucceed()
        {
            // Arrange
            var sut = TrainerFactory.WithLevel(2);
            var arena = ArenaFactory.WithoutRequirement();

            // Act
            var result = sut.EnrollIn(arena);

            // Assert
            result.IsSuccess.Should().BeTrue();
            sut.Enrollment.Value.Should().Be(arena.Id);
            sut.Events.Should().Contain(e => e is TrainerEnrolledEvent);
        }

        [Fact]
        public void Given_LeaveArena_When_TrainerIsNotEnrolled_Then_ShouldFail()
        {
            // Arrange
            var sut = TrainerFactory.WithLevel(1);

            // Act
            var result = sut.LeaveArena();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.TrainerIsNotEnrolled);
            sut.Events.Should().NotContain(e => e is TrainerLeftArenaEvent);
        }

        [Fact]
        public void Given_LeaveArena_When_TrainerIsEnrolled_Then_ShouldLeave()
        {
            // Arrange
            var sut = TrainerFactory.Enrolled();

            // Act
            var result = sut.LeaveArena();

            // Assert
            result.IsSuccess.Should().BeTrue();
            sut.Enrollment.HasNoValue.Should().BeTrue();
            sut.Events.Should().ContainSingle(e => e is TrainerLeftArenaEvent);
        }

        [Fact]
        public void Given_Challenge_When_IsSelf_Then_ShouldFail()
        {
            // Arrange
            var challenger = TrainerFactory.WithLevel(1);
            var pokemonId = challenger.FirstPokemonId();

            // Act
            var result = challenger.Challenge(challenger, pokemonId, pokemonId);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.CannotChallengeSelf);
            challenger.Challenges.Should().BeEmpty();
            challenger.Events.Should().NotContain(e => e is TrainerChallengedEvent);
            challenger.Events.Should().NotContain(e => e is TrainerHasBeenChallengedEvent);
        }

        [Fact]
        public void Given_Challenge_When_ChallengeAlreadyExists_Then_ShouldFail()
        {
            // Arrange
            var challenger = TrainerFactory.WithLevel(1);
            var challenged = TrainerFactory.ChallengedBy(challenger);

            // Act
            var result = challenger.Challenge(challenged, challenger.FirstPokemonId(), challenged.FirstPokemonId());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.TrainerAlreadyChallenged);
            challenger.Challenges.Should().HaveCount(1);
            challenged.Challenges.Should().HaveCount(1);
            challenger.Events.Should().NotContain(e => e is TrainerChallengedEvent);
            challenged.Events.Should().NotContain(e => e is TrainerHasBeenChallengedEvent);
        }

        [Fact]
        public void Given_Challenge_When_TrainerDoesNotOwnPokemon_Then_ShouldFail()
        {
            // Arrange
            var arena = ArenaFactory.WithoutRequirement();
            var challenger = TrainerFactory.EnrolledIn(arena);
            var challenged = TrainerFactory.EnrolledIn(arena);

            // Act
            var result = challenger.Challenge(challenged, challenger.FirstPokemonId(), Guid.NewGuid());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.TrainerDoesNotOwnPokemon);
            challenger.Challenges.Should().BeEmpty();
            challenger.Events.Should().NotContain(e => e is TrainerChallengedEvent);
            challenger.Events.Should().NotContain(e => e is TrainerHasBeenChallengedEvent);
        }

        [Fact]
        public void Given_Challenge_When_ChallengeDoesNotExist_Then_ShouldChallenge()
        {
            // Arrange
            var arena = ArenaFactory.WithoutRequirement();
            var challenger = TrainerFactory.EnrolledIn(arena);
            var challenged = TrainerFactory.EnrolledIn(arena);

            // Act
            var result = challenger.Challenge(challenged, challenger.FirstPokemonId(), challenged.FirstPokemonId());

            // Assert
            result.IsSuccess.Should().BeTrue();
            challenger.Challenges.Should().Contain(c => c.ChallengerId == challenger.Id && c.ChallengedId == challenged.Id && c.ArenaId == arena.Id);
            challenged.Challenges.Should().Contain(c => c.ChallengerId == challenger.Id && c.ChallengedId == challenged.Id && c.ArenaId == arena.Id);
            challenger.Events.Should().ContainSingle(e => e is TrainerChallengedEvent);
            challenged.Events.Should().ContainSingle(e => e is TrainerHasBeenChallengedEvent);
        }

        [Fact]
        public void Given_AcceptChallenge_When_ChallengerDoesNotExist_Then_ShouldFail()
        {
            // Arrange
            var challenged = TrainerFactory.WithLevel(3);

            // Act
            var result = challenged.AcceptChallenge(null, null);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.InvalidTrainer);
            challenged.Events.Should().NotContain(e => e is TrainerAcceptedChallengeEvent);
        }

        [Fact]
        public void Given_AcceptChallenge_When_ChallengeDoesNotExist_Then_ShouldFail()
        {
            // Arrange
            var challenger = TrainerFactory.WithLevel(2);
            var challenged = TrainerFactory.WithLevel(3);

            // Act
            var result = challenged.AcceptChallenge(challenger, null);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.ChallengeNotFound);
            challenger.Events.Should().NotContain(e => e is TrainerChallengeAnsweredEvent);
            challenged.Events.Should().NotContain(e => e is TrainerAcceptedChallengeEvent);
        }

        [Fact]
        public void Given_AcceptChallenge_When_ChallengeIsNotFound_Then_ShouldFail()
        {
            // Arrange
            var challenger = TrainerFactory.WithLevel(2);
            var challenged = TrainerFactory.WithLevel(3);
            var otherChallenged = TrainerFactory.ChallengedBy(challenger);

            // Act
            var result = challenged.AcceptChallenge(challenger, otherChallenged.Challenges.First());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.ChallengeNotFound);
            challenger.Events.Should().NotContain(e => e is TrainerChallengeAnsweredEvent);
            challenged.Events.Should().NotContain(e => e is TrainerAcceptedChallengeEvent);
        }

        [Fact]
        public void Given_AcceptChallenge_When_ChallengeExpired_Then_ShouldFail()
        {
            // Arrange
            var challenger = TrainerFactory.WithLevel(2);
            var challenged = TrainerFactory.ChallengedBy(challenger);
            DateTimeProviderContext.AdvanceUtcTimeTo(challenged.Challenges.First().ExpiresAt.Add(TimeSpan.FromHours(1)));

            // Act
            var result = challenged.AcceptChallenge(challenger, challenged.Challenges.First());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.ChallengeExpired);
            challenger.Events.Should().NotContain(e => e is TrainerChallengeAnsweredEvent);
            challenged.Events.Should().NotContain(e => e is TrainerAcceptedChallengeEvent);
        }

        [Fact]
        public void Given_AcceptChallenge_When_OneIsAlreadyInBattle_Then_ShouldFail()
        {
            true.Should().BeFalse();
        }

        [Fact]
        public void Given_AcceptChallenge_When_ChallengeAlreadyAnswered_Then_ShouldFail()
        {
            // Arrange
            var challenger = TrainerFactory.WithLevel(2);
            var challenged = TrainerFactory.WithChallengeAcceptedFrom(challenger);

            // Act
            var result = challenged.AcceptChallenge(challenger, challenged.Challenges.First());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.ChallengeAlreadyAnswered);
            challenger.Events.Should().NotContain(e => e is TrainerChallengeAnsweredEvent);
            challenged.Events.Should().NotContain(e => e is TrainerAcceptedChallengeEvent);
        }

        [Fact]
        public void Given_AcceptChallenge_When_ChallengeIsPending_Then_ShouldAccept()
        {
            // Arrange
            var challenger = TrainerFactory.WithLevel(2);
            var challenged = TrainerFactory.ChallengedBy(challenger);

            // Act
            var result = challenged.AcceptChallenge(challenger, challenged.Challenges.First());

            // Assert
            result.IsSuccess.Should().BeTrue();
            challenged.Challenges.Should().ContainSingle(c => c.Status == ChallengeStatus.Accepted);
            challenger.Events.Should().ContainSingle(e => e is TrainerChallengeAnsweredEvent);
            challenged.Events.Should().ContainSingle(e => e is TrainerAcceptedChallengeEvent);
        }
    }
}