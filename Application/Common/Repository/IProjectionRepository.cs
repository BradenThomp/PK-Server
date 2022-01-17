using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Repository
{
    public interface IProjectionRepository<T>
    {
        Task<T> GetAsync<Tid>(Tid id);

        Task<IEnumerable<T>> GetAllAsync();
    }
}
