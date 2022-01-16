using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Repository
{
    public interface ICRUDRepository<T>
    {
        Task<T> GetAsync<Tid>(Tid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<int> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
