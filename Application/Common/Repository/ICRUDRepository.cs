using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Repository
{
    /// <summary>
    /// Represents a repository for performing generic CRUD
    /// (Create, Read, Update, Delete) operations.
    /// </summary>
    /// <typeparam name="T">The type of the entity used for the repository.</typeparam>
    public interface ICRUDRepository<T>
    {
        /// <summary>
        /// Gets a entity by id.
        /// </summary>
        /// <typeparam name="Tid">The type of id to select the object by.</typeparam>
        /// <param name="id">The id to select the object by.</param>
        /// <returns>The entity with the given id.</returns>
        Task<T> GetAsync<Tid>(Tid id);

        /// <summary>
        /// Gets all entities in the repository.
        /// </summary>
        /// <returns>All entities in the repository.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task so the operation can be completed asynchronously.</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an entity that exists in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task so the operation can be completed asynchronously.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Removes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>A task so the operation can be completed asynchronously.</returns>
        Task DeleteAsync(T entity);
    }
}
