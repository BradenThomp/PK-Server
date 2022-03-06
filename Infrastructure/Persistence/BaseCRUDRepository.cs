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
    public abstract class BaseCRUDRepository<T> : ICRUDRepository<T>
    {
        protected readonly IConfiguration _configuration;

        public BaseCRUDRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected async Task Commit(Func<DbConnection, Task> func)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await func.Invoke(connection);
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

        public abstract Task AddAsync(T entity);
        public abstract Task DeleteAsync(T entity);
        public abstract Task<IEnumerable<T>> GetAllAsync();
        public abstract Task<T> GetAsync<Tid>(Tid id);
        public abstract Task UpdateAsync(T entity);
    }
}
