using System.Threading.Tasks;
using Pokens.Battles.Domain;

namespace Pokens.Battles.Business
{
    public interface IBattlesNotifications
    {
        Task NotifyBattleStarted(BattleStartedEvent @event);
        Task NotifyCooldownChanged(PlayerCooldownChangedEvent @event);
        Task NotifyTookTurn(PlayerTookTurnEvent @event);
        Task NotifyHealthChanged(BattleHealthChangedEvent @event);
        Task NotifyDodged(PokemonDodgedAbilityEvent @event);
        Task NotifyBattleLost(TrainerLostBattleEvent @event);
        Task NotifyBattleWon(TrainerWonBattleEvent @event);
    }
}