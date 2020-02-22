using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using Pokens.Trainers.Domain;

namespace Pokemons.Trainers.Business
{
    public sealed class AuthenticateTrainerCommand : IRequest<Result<AuthenticationToken>>
    {
        public AuthenticateTrainerCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }

    internal sealed class AuthenticateTrainerCommandHandler : IRequestHandler<AuthenticateTrainerCommand, Result<AuthenticationToken>>
    {
        private readonly IUsersService usersService;
        private readonly ITokenService tokenService;

        public AuthenticateTrainerCommandHandler(IUsersService usersService, ITokenService tokenService)
        {
            EnsureArg.IsNotNull(usersService);
            EnsureArg.IsNotNull(tokenService);
            this.usersService = usersService;
            this.tokenService = tokenService;
        }

        public Task<Result<AuthenticationToken>> Handle(AuthenticateTrainerCommand request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);
            return this.usersService.GetByCredentials(request.Email, request.Password)
                .Bind(c => this.tokenService.Exchange(c));
        }
    }
}