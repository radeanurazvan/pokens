using FluentAssertions;
using Pokens.Battles.Resources;
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
    }
}