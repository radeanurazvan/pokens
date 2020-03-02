using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    internal sealed class AutoModeTrainerHasBeenChallengedEventHandler : IDomainEventHandler<TrainerHasBeenChallengedEvent>
    {
        private readonly IRepositoryMediator mediator;
        private readonly ILogger logger;

        public AutoModeTrainerHasBeenChallengedEventHandler(IRepositoryMediator mediator, ILogger<AutoModeTrainerHasBeenChallengedEventHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Handle(TrainerHasBeenChallengedEvent @event)
        {
            var arenaResult = await mediator.ReadById<Arena>(@event.ArenaId).ToResult(Messages.ArenaNotFound);
            var challengerResult = await mediator.ReadById<Trainer>(@event.ChallengerId).ToResult(Messages.TrainerNotFound);
            var challengedResult = await mediator.ReadById<Trainer>(@event.TrainerId).ToResult(Messages.TrainerNotFound);

            await Result.FirstFailureOrSuccess(arenaResult, challengedResult, challengerResult)
                .Ensure(() => challengedResult.Value.IsAutoMode, Messages.TrainerNotInAutoMode)
                .Bind(() => arenaResult.Value.MediateChallengeApproval(challengerResult.Value, challengedResult.Value, @event.ChallengeId))
                .Tap(() => mediator.Write<Arena>().Save())
                .OnFailure(e => this.logger.LogWarning($"Failed with message {e}"));
        }
    }
}