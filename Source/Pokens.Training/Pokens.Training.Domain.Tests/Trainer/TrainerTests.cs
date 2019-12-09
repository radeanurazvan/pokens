using FluentAssertions;
using Xunit;

namespace Pokens.Training.Domain.Tests
{
    public class TrainerTests
    {
        [Fact]
        public void Given_ChooseStarter_When_DefinitionIsNull_Then_ShouldFail()
        {
            // Arrange
            var sut = TrainerFactory.Ash();

            // Act
            var result = sut.ChooseStarter(null);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Given_ChooseStarter_When_DefinitionIsNotStarter_Then_ShouldFail()
        {
            // Arrange
            var sut = TrainerFactory.Ash();

            // Act
            var result = sut.ChooseStarter(PokemonDefinitionFactory.NotStarter());

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Given_ChooseStarter_When_TrainerAlreadyHasStarter_Then_ShouldFail()
        {
            // Arrange
            var sut = TrainerFactory.AshWithPikachu();

            // Act
            var result = sut.ChooseStarter(PokemonDefinitionFactory.Starter());

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Given_ChooseStarter_When_TrainerDoesNotHaveStarter_Then_ShouldSuccessfullyChoose()
        {
            // Arrange
            var sut = TrainerFactory.Ash();
            var pikachu = PokemonDefinitionFactory.Starter("Pikachu");

            // Act
            var result = sut.ChooseStarter(pikachu);

            // Assert
            result.IsFailure.Should().BeTrue();
        }
    }
}