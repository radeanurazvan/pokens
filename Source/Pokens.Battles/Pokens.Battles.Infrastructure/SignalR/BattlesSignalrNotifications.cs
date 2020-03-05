using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Pokens.Battles.Business;
using Pokens.Battles.Domain;

namespace Pokens.Battles.Infrastructure
{
    internal sealed class BattlesSignalrNotifications : IBattlesNotifications
    {
        private readonly IHubContext<BattlesHub> hubContext;

        public BattlesSignalrNotifications(IHubContext<BattlesHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public Task NotifyBattleStarted(BattleStartedEvent @event)
        {
            return Task.WhenAll(
                this.hubContext.Clients.Group(@event.AttackerId.ToString()).SendAsync(nameof(BattleStartedEvent)),
                this.hubContext.Clients.Group(@event.DefenderId.ToString()).SendAsync(nameof(BattleStartedEvent)));
        }

        public Task NotifyCooldownChanged(PlayerCooldownChangedEvent @event)
        {
            return this.hubContext.Clients.Group(@event.BattleId.ToString()).SendAsync(nameof(PlayerCooldownChangedEvent), @event);
        }

        public Task NotifyTookTurn(PlayerTookTurnEvent @event)
        {
            return this.hubContext.Clients.Group(@event.BattleId.ToString()).SendAsync(nameof(PlayerTookTurnEvent), @event);
        }

        public Task NotifyHealthChanged(BattleHealthChangedEvent @event)
        {
            return this.hubContext.Clients.Group(@event.BattleId.ToString()).SendAsync(nameof(BattleHealthChangedEvent), @event);
        }

        public Task NotifyDodged(PokemonDodgedAbilityEvent @event)
        {
            return this.hubContext.Clients.Group(@event.BattleId.ToString()).SendAsync(nameof(PokemonDodgedAbilityEvent), @event);
        }

        public Task NotifyBattleLost(TrainerLostBattleEvent @event)
        {
            return this.hubContext.Clients.Group(@event.BattleId.ToString()).SendAsync(nameof(TrainerLostBattleEvent), @event);
        }

        public Task NotifyBattleWon(TrainerWonBattleEvent @event)
        {
            return this.hubContext.Clients.Group(@event.BattleId.ToString()).SendAsync(nameof(TrainerWonBattleEvent), @event);
        }
    }
}