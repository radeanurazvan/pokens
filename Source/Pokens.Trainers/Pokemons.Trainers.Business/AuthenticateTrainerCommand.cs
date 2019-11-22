using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using Pokens.Trainers.Domain;
using Pomelo.Kernel.Common;

namespace Pokemons.Trainers.Business
{
    public sealed class AuthenticateTrainerCommand : ICommand<AuthenticationToken>
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
        private readonly ICredentialsService credentialsService;
        private readonly ITokenService tokenService;

        public AuthenticateTrainerCommandHandler(ICredentialsService credentialsService, ITokenService tokenService)
        {
            EnsureArg.IsNotNull(credentialsService);
            EnsureArg.IsNotNull(tokenService);
            this.credentialsService = credentialsService;
            this.tokenService = tokenService;
        }

        public Task<Result<AuthenticationToken>> Handle(AuthenticateTrainerCommand request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);
            return this.credentialsService.GetByTuple(request.Email, request.Password)
                .Bind(c => this.tokenService.Exchange(c));
        }
    }
}