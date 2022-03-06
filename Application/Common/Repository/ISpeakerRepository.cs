using Domain.Models;

namespace Application.Common.Repository
{
    /// <summary>
    /// A CRUD repository for the <see cref="Speaker" entity/>.
    /// <inheritdoc/>
    /// </summary>
    public interface ISpeakerRepository : ICRUDRepository<Speaker>{}
}
