using FluentAssertions;
using Xunit;
using System.Linq;

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
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Given_CatchPokemon_When_DefinitionIsNull_Then_ShouldFail()
        {
            // Arrange
            var sut = TrainerFactory.Ash();

            // Act
            var result = sut.CatchPokemon(null);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Given_CatchPokemon_When_DefinitionAlreadyExists_Then_ShouldFail()
        {
            // Arrange
            var sut = TrainerFactory.Ash();
            var pikachu = PokemonDefinitionFactory.GetPokemonDefinition("Pikachu");
            sut.CatchPokemon(pikachu);

            // Act
            var result = sut.CatchPokemon(pikachu);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Given_CatchPokemon_When_DefinitionIsNotNullAndDoesNotAlreadyExist_Then_ShouldSuccessfullyCatch()
        {
            // Arrange
            var sut = TrainerFactory.Ash();
            var pikachu = PokemonDefinitionFactory.GetPokemonDefinition("Pikachu");

            // Act
            var result = sut.CatchPokemon(pikachu);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}