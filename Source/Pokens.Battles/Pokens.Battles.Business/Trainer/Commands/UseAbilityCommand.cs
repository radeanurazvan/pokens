using System;
using Pomelo.Kernel.Common;

namespace Pokens.Battles.Business
{
    public sealed class UseAbilityCommand : ICommand
    {
        public UseAbilityCommand(Guid battleId, Guid trainerId, Guid abilityId)
        {
            BattleId = battleId;
            TrainerId = trainerId;
            AbilityId = abilityId;
        }

        public Guid BattleId { get; }
        
        public Guid TrainerId { get; }

        public Guid AbilityId { get; }
    }
}