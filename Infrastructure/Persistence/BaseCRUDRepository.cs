using Application.Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
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

        public abstract Task AddAsync(T entity);
        public abstract Task DeleteAsync(T entity);
        public abstract Task<IEnumerable<T>> GetAllAsync();
        public abstract Task<T> GetAsync<Tid>(Tid id);
        public abstract Task UpdateAsync(T entity);
    }
}
