using System.Threading.Tasks;

namespace Application.Common.Notifications
{
    public interface INotificationService
    {
        public Task Notify(INotification notification);
    }
}
