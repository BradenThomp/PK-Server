using Application.Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    /// <inheritdoc/>
    public abstract class BaseCRUDRepository<T> : ICRUDRepository<T>
    {
        protected readonly IConfiguration _configuration;

        public BaseCRUDRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Commits a unit of work to the database. If an error occurs, the entire unit of work is rolled back.
        /// </summary>
        /// <param name="func">The unit of work.</param>
        /// <returns>A task so the unit of work can be completed async.</returns>
        protected async Task Commit(Func<DbConnection, Task> uow)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await uow.Invoke(connection);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public abstract Task AddAsync(T entity);
        /// <inheritdoc/>
        public abstract Task DeleteAsync(T entity);
        /// <inheritdoc/>
        public abstract Task<IEnumerable<T>> GetAllAsync();
        /// <inheritdoc/>
        public abstract Task<T> GetAsync<Tid>(Tid id);
        /// <inheritdoc/>
        public abstract Task UpdateAsync(T entity);
    }
}
