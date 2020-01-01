using FluentAssertions;
using Pokens.Training.Resources;
using Xunit;
using Pomelo.Kernel.Domain;

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
            result.Error.Should().Be(Messages.InvalidPokemonDefinition);
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
            result.Error.Should().Be(Messages.PokemonNotStarter);
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
            result.Error.Should().Be(Messages.TrainerAlreadyHasStarter);
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
        public void Given_CatchPokemon_When_DefinitionDoesNotExist_Then_ShouldFail()
        {
            // Arrange
            var sut = TrainerFactory.Ash();

            // Act
            var result = sut.CatchPokemon(null);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.InvalidPokemonDefinition);
        }

        [Fact]
        public void Given_CatchPokemon_When_PokemonAlreadyCaught_Then_ShouldFail()
        {
            // Arrange
            var sut = TrainerFactory.Ash();
            var pikachu = PokemonDefinitionFactory.GetPokemonDefinition("Pikachu");
            sut.CatchPokemon(pikachu);

            // Act
            var result = sut.CatchPokemon(pikachu);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.TrainerAlreadyHasThisPokemon);
        }

        [Fact]
        public void Given_CatchPokemon_When_PokemonNotCaughtAndCatchRateSucceeds_Then_ShouldSuccessfullyCatch()
        {
            // Arrange
            var sut = TrainerFactory.Ash();
            var pikachu = PokemonDefinitionFactory.GetPokemonDefinition("Pikachu", 90);
            RandomProviderContext.PredictDouble(0.89);

            // Act
            var result = sut.CatchPokemon(pikachu);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Given_CatchPokemon_When_PokemonNotCaughtAndCatchRateFails_Then_ShouldFail()
        {
            // Arrange
            var sut = TrainerFactory.Ash();
            var pikachu = PokemonDefinitionFactory.GetPokemonDefinition("Pikachu", 1);
            RandomProviderContext.PredictDouble(0.91);

            // Act
            var result = sut.CatchPokemon(pikachu);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(Messages.CatchFailed);
        }
    }
}