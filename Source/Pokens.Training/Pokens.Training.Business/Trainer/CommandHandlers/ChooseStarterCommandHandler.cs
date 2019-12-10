using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using Pokens.Training.Domain;
using Pokens.Training.Resources;
using Pomelo.Kernel.Domain;

namespace Pokens.Training.Business
{
    internal sealed class ChooseStarterCommandHandler : IRequestHandler<ChooseStarterCommand, Result>
    {
        private readonly ICollectionRepository repository;

        public ChooseStarterCommandHandler(ICollectionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result> Handle(ChooseStarterCommand request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);
            var trainerResult = await repository.FindOne<Trainer>(t => t.Id == request.TrainerId).ToResult(Messages.TrainerNotFound);
            var definitionResult = await repository.FindOne<PokemonDefinition>(d => d.Id == request.PokemonId).ToResult(Messages.PokemonNotFound);

            return Result.FirstFailureOrSuccess(trainerResult, definitionResult)
                .Bind(() => trainerResult.Value.ChooseStarter(definitionResult.Value));
        }
    }
}