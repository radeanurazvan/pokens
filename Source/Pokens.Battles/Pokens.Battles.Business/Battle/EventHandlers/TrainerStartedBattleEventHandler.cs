using System.Threading.Tasks;
using Pokens.Battles.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business.Battle.EventHandlers
{
    internal sealed class TrainerStartedBattleEventHandler : IDomainEventHandler<TrainerStartedBattleEvent>
    {
        public Task Handle(TrainerStartedBattleEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}