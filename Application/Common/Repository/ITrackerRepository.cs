using Domain.Models;

namespace Application.Common.Repository
{
    /// <summary>
    /// A CRUD repository for the <see cref="Tracker" entity/>.
    /// <inheritdoc/>
    /// </summary>
    public interface ITrackerRepository : ICRUDRepository<Tracker>{}
}
