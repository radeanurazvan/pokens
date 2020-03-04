using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Pokens.Battles.Infrastructure
{
    internal sealed class BattlesHub : Hub
    {
        public Task JoinBattleNotifications(string battleId)
        {
            return this.Groups.AddToGroupAsync(this.Context.ConnectionId, battleId);
        }
    }
}