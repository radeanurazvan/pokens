using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    internal sealed class BattleService : IBattlesService
    {
        private readonly IRepositoryMediator mediator;

        public BattleService(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result> TakeTurn(Guid battleId, Guid trainerId, Guid abilityId)
        {
            var trainerResult = await mediator.ReadById<Trainer>(trainerId).ToResult(Messages.TrainerNotFound);
            var battleResult = await mediator.ReadById<Battle>(battleId).ToResult(Messages.BattleNotFound);

            return await Result.FirstFailureOrSuccess(trainerResult, battleResult)
                .Bind(() => trainerResult.Value.UseAbilityIn(battleResult.Value, abilityId))
                .Tap(() => mediator.Write<Battle>().Save());
        }
    }
}