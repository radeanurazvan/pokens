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
        private readonly IGetById<Arena> arenasReadRepository;
        private readonly IGetById<Trainer> trainersReadRepository;
        private readonly IWriteRepository<Arena> arenasWriteRepository;

        public AcceptChallengeCommandHandler(IGetById<Arena> arenasReadRepository, IGetById<Trainer> trainersReadRepository, IWriteRepository<Arena> arenasWriteRepository)
        {
            this.arenasReadRepository = arenasReadRepository;
            this.trainersReadRepository = trainersReadRepository;
            this.arenasWriteRepository = arenasWriteRepository;
        }

        public async Task<Result> Handle(AcceptChallengeCommand request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);

            var arenaResult = await arenasReadRepository.GetById(request.ArenaId).ToResult(Messages.ArenaNotFound);
            var challengeResult = arenaResult.Bind(a => a.Challenges.FirstOrNothing(c => c.Id == request.ChallengeId).ToResult(Messages.ChallengeNotFound));
            var challengerResult = await challengeResult.Bind(c => trainersReadRepository.GetById(c.ChallengerId).ToResult(Messages.TrainerNotFound));
            var challengedResult = await challengeResult.Bind(c => trainersReadRepository.GetById(c.ChallengedId).ToResult(Messages.TrainerNotFound));

            return await Result.FirstFailureOrSuccess(arenaResult, challengedResult, challengerResult)
                .Tap(() => arenaResult.Value.MediateChallengeApproval(challengerResult.Value, challengedResult.Value, request.ChallengeId))
                .Tap(() => this.arenasWriteRepository.Save());
        }
    }
}