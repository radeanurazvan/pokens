using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Business
{
    internal sealed class AcceptChallengeCommandHandler : IRequestHandler<AcceptChallengeCommand, Result>
    {
        private readonly IRepositoryMediator mediator;

        public AcceptChallengeCommandHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result> Handle(AcceptChallengeCommand request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);

            var arenaResult = await mediator.ReadById<Arena>(request.ArenaId).ToResult(Messages.ArenaNotFound);
            var challengeResult = arenaResult.Bind(a => a.Challenges.TryFirst(c => c.Id == request.ChallengeId).ToResult(Messages.ChallengeNotFound));
            var challengerResult = await challengeResult.Bind(c => mediator.ReadById<Trainer>(c.ChallengerId).ToResult(Messages.TrainerNotFound));
            var challengedResult = await challengeResult.Bind(c => mediator.ReadById<Trainer>(c.ChallengedId).ToResult(Messages.TrainerNotFound));

            return await Result.FirstFailureOrSuccess(arenaResult, challengedResult, challengerResult)
                .Bind(() => arenaResult.Value.MediateChallengeApproval(challengerResult.Value, challengedResult.Value, request.ChallengeId))
                .Tap(() => this.mediator.Write<Arena>().Save());
        }
    }
}