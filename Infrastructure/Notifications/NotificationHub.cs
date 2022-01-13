using Application.Common.Notifications;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Infrastructure.Notifications
{
    public class NotificationHub : Hub, INotificationService
    {
        private readonly IHubContext<NotificationHub> _context;
        public NotificationHub(IHubContext<NotificationHub> context)
        {
            _context = context;
        }

        public async Task Notify(INotification notification)
        {
            var json = JsonConvert.SerializeObject(notification);
            await _context.Clients.All.SendAsync("Notification", json);
        }

    }
}
