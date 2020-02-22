using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Business
{
    internal sealed class EnrollTrainerCommandHandler : IRequestHandler<EnrollTrainerCommand, Result>
    {
        private readonly IRepositoryMediator mediator;

        public EnrollTrainerCommandHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result> Handle(EnrollTrainerCommand request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);

            var arenaResult = await this.mediator.Read<Arena>().GetById(request.ArenaId).ToResult(Messages.ArenaNotFound);
            var trainerResult = await this.mediator.Read<Trainer>().GetById(request.TrainerId).ToResult(Messages.TrainerNotFound);

            return await Result.FirstFailureOrSuccess(arenaResult, trainerResult)
                .Tap(() => arenaResult.Value.Enroll(trainerResult.Value))
                .Tap(() => mediator.Write<Arena>().Save());
        }
    }
}