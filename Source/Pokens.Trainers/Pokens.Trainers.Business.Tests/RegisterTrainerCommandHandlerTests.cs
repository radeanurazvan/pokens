using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Pokemons.Trainers.Business;
using Pokens.Trainers.Domain;
using Pomelo.Kernel.Domain;
using Xunit;

namespace Pokens.Trainers.Business.Tests
{
    public class RegisterTrainerCommandHandlerTests
    {
        private readonly Mock<IWriteRepository<Trainer>> repositoryMock;
        private readonly Mock<ICredentialsService> credentialsServiceMock;

        public RegisterTrainerCommandHandlerTests()
        {
            repositoryMock = new Mock<IWriteRepository<Trainer>>();
            credentialsServiceMock = new Mock<ICredentialsService>();
        }

        [Fact]
        public void When_CreateTrainerFails_Then_ShouldNotAddTrainer()
        {
            var command = new RegisterTrainerCommand("", "email@mail.com", "strongpassword");
            var sut = GetHandler();

            var result = sut.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

            result.IsFailure.Should().BeTrue();
            repositoryMock.Verify(rm => rm.Add(It.IsAny<Trainer>()), Times.Never);
            repositoryMock.Verify(rm => rm.Save(), Times.Never);
        }

        [Fact]
        public void When_CreateTrainerFails_Then_ShouldNotCreateCredentials()
        {
            var command = new RegisterTrainerCommand("", "email@mail.com", "strongpassword");
            var sut = GetHandler();

            var result = sut.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

            result.IsFailure.Should().BeTrue();
            credentialsServiceMock.Verify(cs => cs.Create(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void When_TrainerCreatedButCreateCredentialsFail_Then_ShouldNotAddTrainer()
        {
            var command = new RegisterTrainerCommand("Ash", "", "");
            this.credentialsServiceMock.Setup(cs => cs.Create(It.IsAny<Guid>(), command.Email, command.Password)).ReturnsAsync(Result.Failure("Failure"));
            var sut = GetHandler();

            var result = sut.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

            result.IsFailure.Should().BeTrue();
            repositoryMock.Verify(rm => rm.Add(It.IsAny<Trainer>()), Times.Never);
            repositoryMock.Verify(rm => rm.Save(), Times.Never);
        }

        [Fact]
        public void When_TrainerAndCredentialsCreated_Then_ShouldSave()
        {

            var command = new RegisterTrainerCommand("Ash", "ash@hotmail.com", "AshThebest123!23#@!");
            this.credentialsServiceMock.Setup(cs => cs.Create(It.IsAny<Guid>(), command.Email, command.Password)).ReturnsAsync(Result.Ok);
            this.repositoryMock.Setup(rm => rm.Add(It.IsAny<Trainer>())).Returns(Task.CompletedTask);
            var sut = GetHandler();

            var result = sut.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

            result.IsSuccess.Should().BeTrue();
            repositoryMock.Verify(rm => rm.Add(It.IsAny<Trainer>()), Times.Once);
            repositoryMock.Verify(rm => rm.Save(), Times.Once);
        }

        private RegisterTrainerCommandHandler GetHandler() => new RegisterTrainerCommandHandler(this.credentialsServiceMock.Object, this.repositoryMock.Object);
    }
}
