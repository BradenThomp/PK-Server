using System.Threading.Tasks;

namespace Application.Common.Notifications
{
    /// <summary>
    /// Dispatches notifications to all listening clients.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Notifies all clients of a notification.
        /// </summary>
        /// <param name="notification">The notification to be sent to all clients.</param>
        /// <returns>A task so clients can be notified asynchronously.</returns>
        public Task Notify(INotification notification);
    }
}
