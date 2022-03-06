using Domain.Models;

namespace Application.Common.Repository
{
    /// <summary>
    /// A CRUD repository for the <see cref="Rental" entity/>.
    /// <inheritdoc/>
    /// </summary>
    public interface IRentalRepository : ICRUDRepository<Rental>{}
}
