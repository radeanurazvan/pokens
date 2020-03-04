using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Business
{
    internal sealed class RejectChallengeCommandHandler : IRequestHandler<RejectChallengeCommand, Result>
    {
        private readonly IRepositoryMediator mediator;

        public RejectChallengeCommandHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task<Result> Handle(RejectChallengeCommand request, CancellationToken cancellationToken)
        {
            return mediator.ReadById<Trainer>(request.TrainerId).ToResult(Messages.TrainerNotFound)
                .Bind(t => t.RejectChallenge(request.ChallengeId))
                .Tap(() => mediator.Write<Trainer>().Save());
        }
    }
}