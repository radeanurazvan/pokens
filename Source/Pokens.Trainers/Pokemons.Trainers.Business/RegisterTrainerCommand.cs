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
        private readonly ICredentialsService credentialsService;
        private readonly IWriteRepository<Trainer> writeRepository;

        public RegisterTrainerCommandHandler(ICredentialsService credentialsService, IWriteRepository<Trainer> writeRepository)
        {
            EnsureArg.IsNotNull(credentialsService);
            EnsureArg.IsNotNull(writeRepository);
            this.credentialsService = credentialsService;
            this.writeRepository = writeRepository;
        }

        public async Task<Result> Handle(RegisterTrainerCommand request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);

            var trainerResult = Trainer.Create(request.Name);
            return await trainerResult
                .Bind(t => this.credentialsService.Create(t.Id, request.Email, request.Password))
                .Bind(() => trainerResult)
                .Tap(t => this.writeRepository.Add(t))
                .Tap(_ => this.writeRepository.Save());
        }
    }
}