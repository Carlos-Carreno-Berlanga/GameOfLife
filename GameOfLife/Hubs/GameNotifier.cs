using GameOfLife.Dtos;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GameOfLife.Hubs
{
    public class GameNotifier : Hub
    {
        public Task Notify(GameStatusDto gameStatus)
        {
            return Clients.All.SendAsync("GameStatus", gameStatus);
        }
    }
}
