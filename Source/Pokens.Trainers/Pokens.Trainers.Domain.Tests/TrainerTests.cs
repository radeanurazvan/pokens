using FluentAssertions;
using Xunit;

namespace Pokens.Trainers.Domain.Tests
{
    public class TrainerTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Given_Create_When_NameIsInvalid_Then_ShouldFail(string name)
        {
            var result = Trainer.Create(name);

            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Given_Create_When_NameIsValid_Then_ShouldSuccessfullyCreateTrainer()
        {
            var result = Trainer.Create("Ash");

            result.IsSuccess.Should().BeTrue();
            result.Value.Name.Should().Be("Ash");
        }

        [Fact]
        public void Given_Create_When_NameIsValid_Then_ShouldAssignName()
        {
            var result = Trainer.Create("Ash");

            result.IsSuccess.Should().BeTrue();
            result.Value.Name.Should().Be("Ash");
        }

        [Fact]
        public void Given_Create_When_NameIsValid_Then_ShouldAddEvent()
        {
            var result = Trainer.Create("Ash");

            result.IsSuccess.Should().BeTrue();
            result.Value.Events.Should().HaveCount(1);
            result.Value.Events.Should().ContainSingle(e => e is TrainerCreatedEvent);
        }
    }
}
