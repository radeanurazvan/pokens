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
    internal sealed class ChallengeTrainerCommandHandler : IRequestHandler<ChallengeTrainerCommand, Result>
    {
        private readonly IRepositoryMediator mediator;

        public ChallengeTrainerCommandHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result> Handle(ChallengeTrainerCommand request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);

            var arenaResult = await mediator.ReadById<Arena>(request.ArenaId).ToResult(Messages.ArenaNotFound);
            var challengerResult = await mediator.ReadById<Trainer>(request.ChallengerId).ToResult(Messages.TrainerNotFound);
            var challengedResult = await mediator.ReadById<Trainer>(request.ChallengedId).ToResult(Messages.TrainerNotFound);

            return await Result.FirstFailureOrSuccess(arenaResult, challengerResult, challengedResult)
                .Bind(() => arenaResult.Value.MediateChallenge(challengerResult.Value, request.ChallengerPokemonId, challengedResult.Value, request.ChallengedPokemonId))
                .Tap(() => this.mediator.Write<Arena>().Save());
        }
    }
}