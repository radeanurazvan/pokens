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
    internal sealed class EndTrainerEnrollmentCommandHandler : IRequestHandler<EndTrainerEnrollmentCommand, Result>
    {
        private readonly IRepositoryMediator mediator;

        public EndTrainerEnrollmentCommandHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result> Handle(EndTrainerEnrollmentCommand request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);

            var arenaResult = await this.mediator.Read<Arena>().GetById(request.ArenaId).ToResult(Messages.ArenaNotFound);
            var trainerResult = await this.mediator.Read<Trainer>().GetById(request.TrainerId).ToResult(Messages.TrainerNotFound);

            return await Result.FirstFailureOrSuccess(arenaResult, trainerResult)
                .Tap(() => arenaResult.Value.EndEnrollmentFor(trainerResult.Value))
                .Tap(() => this.mediator.Write<Arena>().Save());
        }
    }
}