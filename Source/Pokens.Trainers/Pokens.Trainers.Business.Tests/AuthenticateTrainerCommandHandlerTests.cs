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
        private readonly Mock<IUsersService> usersServiceMock;
        private readonly Mock<ITokenService> tokenServiceMock;

        public AuthenticateTrainerCommandHandlerTests()
        {
            this.usersServiceMock = new Mock<IUsersService>();
            this.tokenServiceMock = new Mock<ITokenService>();
        }

        [Fact]
        public void When_CredentialsDoNotExist_Then_ShouldFailWithoutExchange()
        {
            var command = new AuthenticateTrainerCommand("asd", "dsa");
            this.usersServiceMock.Setup(cs => cs.GetByCredentials(command.Email, command.Password)).ReturnsAsync(Result.Failure<User>("Fail"));
            var sut = GetHandler();

            var result = sut.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

            result.IsFailure.Should().BeTrue();
            this.tokenServiceMock.Verify(ts => ts.Exchange(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public void When_ExchangeFails_Then_ShouldFail()
        {
            var credentials = new User("mail@mail.com");
            var command = new AuthenticateTrainerCommand("asd", "dsa");
            this.usersServiceMock.Setup(cs => cs.GetByCredentials(command.Email, command.Password)).ReturnsAsync(Result.Ok(credentials));
            this.tokenServiceMock.Setup(ts => ts.Exchange(credentials)).Returns(Result.Failure<AuthenticationToken>("Fail"));
            var sut = GetHandler();

            var result = sut.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void When_CorrectCredentials_Then_ShouldExchangeIntoToken()
        {

            var credentials = new User("mail@mail.com");
            var command = new AuthenticateTrainerCommand("asd", "dsa");
            this.usersServiceMock.Setup(cs => cs.GetByCredentials(command.Email, command.Password)).ReturnsAsync(Result.Ok(credentials));
            this.tokenServiceMock.Setup(ts => ts.Exchange(credentials)).Returns(Result.Ok(new AuthenticationToken("", DateTime.Now)));
            var sut = GetHandler();

            var result = sut.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

            result.IsSuccess.Should().BeTrue();
        }

        private AuthenticateTrainerCommandHandler GetHandler() => new AuthenticateTrainerCommandHandler(this.usersServiceMock.Object, this.tokenServiceMock.Object);
    }
}