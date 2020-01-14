using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Battles.Business
{
    internal sealed class TrainerAcceptedChallengeEventHandler : IBusMessageHandler<TrainerAcceptedChallengeEvent>
    {
        private readonly IGetById<Arena> arenasReadRepository;
        private readonly IGetById<Trainer> trainersReadRepository;
        private readonly IWriteRepository<Trainer> trainersWriteRepository;

        public TrainerAcceptedChallengeEventHandler(IGetById<Arena> arenasReadRepository, IGetById<Trainer> trainersReadRepository, IWriteRepository<Trainer> trainersWriteRepository)
        {
            this.arenasReadRepository = arenasReadRepository;
            this.trainersReadRepository = trainersReadRepository;
            this.trainersWriteRepository = trainersWriteRepository;
        }

        public async Task Handle(TrainerAcceptedChallengeEvent notification)
        {
            EnsureArg.IsNotNull(notification);

            var arenaResult = await arenasReadRepository.GetById(notification.ArenaId).ToResult(Messages.ArenaNotFound);
            var challengeResult = arenaResult.Bind(a => a.Challenges.FirstOrNothing(c => c.Id == notification.ChallengeId).ToResult(Messages.ChallengeNotFound));
            var challengerResult = await challengeResult.Bind(c => trainersReadRepository.GetById(c.ChallengerId).ToResult(Messages.TrainerNotFound));
            var challengedResult = await challengeResult.Bind(c => trainersReadRepository.GetById(c.ChallengedId).ToResult(Messages.TrainerNotFound));

            await Result.FirstFailureOrSuccess(arenaResult, challengedResult, challengerResult)
                .Tap(() => challengerResult.Value.StartBattleAgainst(challengedResult.Value))
                .Tap(() => this.trainersWriteRepository.Save());
        }
    }
}