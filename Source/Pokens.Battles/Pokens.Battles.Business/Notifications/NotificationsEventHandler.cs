using System.Threading.Tasks;
using Pokens.Battles.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    internal sealed class NotificationsEventHandler : IDomainEventHandler<PlayerCooldownChangedEvent>,
        IDomainEventHandler<PlayerTookTurnEvent>,
        IDomainEventHandler<BattleHealthChangedEvent>, 
        IDomainEventHandler<PokemonDodgedAbilityEvent>,
        IDomainEventHandler<TrainerLostBattleEvent>,
        IDomainEventHandler<TrainerWonBattleEvent>
    {
        private readonly IBattlesNotifications notifications;

        public NotificationsEventHandler(IBattlesNotifications notifications)
        {
            this.notifications = notifications;
        }

        public Task Handle(PlayerCooldownChangedEvent @event) => notifications.NotifyCooldownChanged(@event);

        public Task Handle(PlayerTookTurnEvent @event) => notifications.NotifyTookTurn(@event);

        public Task Handle(BattleHealthChangedEvent @event) => notifications.NotifyHealthChanged(@event);

        public Task Handle(PokemonDodgedAbilityEvent @event) => notifications.NotifyDodged(@event);

        public Task Handle(TrainerLostBattleEvent @event) => notifications.NotifyBattleLost(@event);

        public Task Handle(TrainerWonBattleEvent @event) => notifications.NotifyBattleWon(@event);
    }
}