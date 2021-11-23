using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Infrastructure.Notifications
{
    class TrackerHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
