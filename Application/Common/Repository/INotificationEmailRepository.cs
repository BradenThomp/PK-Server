using Domain.Models;

namespace Application.Common.Repository
{
    /// <summary>
    /// A CRUD repository for the <see cref="NotificationEmail" entity/>.
    /// <inheritdoc/>
    /// </summary>
    public interface INotificationEmailRepository : ICRUDRepository<NotificationEmail>
    {
    }
}
