using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Pokens.Battles.Domain;

namespace Pokens.Battles.Business
{
    internal sealed class UseAbilityCommandHandler : IRequestHandler<UseAbilityCommand, Result>
    {
        private readonly IBattlesService battlesService;

        public UseAbilityCommandHandler(IBattlesService battlesService)
        {
            this.battlesService = battlesService;
        }

        public Task<Result> Handle(UseAbilityCommand request, CancellationToken cancellationToken)
        {
            return battlesService.TakeTurn(request.BattleId, request.TrainerId, request.AbilityId);
        }
    }
}