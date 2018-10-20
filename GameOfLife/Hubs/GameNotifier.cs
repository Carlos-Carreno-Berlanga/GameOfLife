using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GameOfLife.Hubs
{
    public class GameNotifier : Hub
    {
        public Task Notify(string message)
        {
            return Clients.All.SendAsync("GameStatus", message);
        }
    }
}
