using System;
using System.Threading;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Pokemons.Trainers.Business;
using Pokens.Trainers.Domain;
using Xunit;

namespace Pokens.Trainers.Business.Tests
{
    public class AuthenticateTrainerCommandHandlerTests
    {
        private readonly Mock<ICredentialsService> credentialsServiceMock;
        private readonly Mock<ITokenService> tokenServiceMock;

        public AuthenticateTrainerCommandHandlerTests()
        {
            this.credentialsServiceMock = new Mock<ICredentialsService>();
            this.tokenServiceMock = new Mock<ITokenService>();
        }

        [Fact]
        public void When_CredentialsDoNotExist_Then_ShouldFailWithoutExchange()
        {
            var command = new AuthenticateTrainerCommand("asd", "dsa");
            this.credentialsServiceMock.Setup(cs => cs.GetByTuple(command.Email, command.Password)).ReturnsAsync(Result.Failure<Credentials>("Fail"));
            var sut = GetHandler();

            var result = sut.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

            result.IsFailure.Should().BeTrue();
            this.tokenServiceMock.Verify(ts => ts.Exchange(It.IsAny<Credentials>()), Times.Never);
        }

        [Fact]
        public void When_ExchangeFails_Then_ShouldFail()
        {
            var credentials = new Credentials("mail@mail.com", new Guid("5401BB01-20FD-4143-8D58-EA7E53248B63"));
            var command = new AuthenticateTrainerCommand("asd", "dsa");
            this.credentialsServiceMock.Setup(cs => cs.GetByTuple(command.Email, command.Password)).ReturnsAsync(Result.Ok(credentials));
            this.tokenServiceMock.Setup(ts => ts.Exchange(credentials)).Returns(Result.Failure<AuthenticationToken>("Fail"));
            var sut = GetHandler();

            var result = sut.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void When_CorrectCredentials_Then_ShouldExchangeIntoToken()
        {

            var credentials = new Credentials("mail@mail.com", new Guid("5401BB01-20FD-4143-8D58-EA7E53248B63"));
            var command = new AuthenticateTrainerCommand("asd", "dsa");
            this.credentialsServiceMock.Setup(cs => cs.GetByTuple(command.Email, command.Password)).ReturnsAsync(Result.Ok(credentials));
            this.tokenServiceMock.Setup(ts => ts.Exchange(credentials)).Returns(Result.Ok(new AuthenticationToken("", DateTime.Now)));
            var sut = GetHandler();

            var result = sut.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

            result.IsSuccess.Should().BeTrue();
        }

        private AuthenticateTrainerCommandHandler GetHandler() => new AuthenticateTrainerCommandHandler(this.credentialsServiceMock.Object, this.tokenServiceMock.Object);
    }
}