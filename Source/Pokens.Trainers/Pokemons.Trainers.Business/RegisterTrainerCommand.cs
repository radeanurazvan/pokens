using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using Pokens.Trainers.Domain;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokemons.Trainers.Business
{
    public sealed class RegisterTrainerCommand : ICommand
    {
        public RegisterTrainerCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; }

        public string Email { get; }

        public string Password { get; }
    }

    internal sealed class RegisterTrainerCommandHandler : IRequestHandler<RegisterTrainerCommand, Result>
    {
        private readonly IUsersService usersService;
        private readonly IWriteRepository<Trainer> writeRepository;

        public RegisterTrainerCommandHandler(IUsersService usersService, IWriteRepository<Trainer> writeRepository)
        {
            EnsureArg.IsNotNull(usersService);
            EnsureArg.IsNotNull(writeRepository);
            this.usersService = usersService;
            this.writeRepository = writeRepository;
        }

        public async Task<Result> Handle(RegisterTrainerCommand request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);
            var user = new User(request.Email);

            return await Trainer.Create(request.Name, user)
                .Bind(_ => this.usersService.Create(user, request.Password))
                .Bind(() => Trainer.Create(request.Name, user))
                .Tap(t => this.writeRepository.Add(t))
                .Tap(_ => this.writeRepository.Save());
        }
    }
}