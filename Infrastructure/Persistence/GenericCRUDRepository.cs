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
    public abstract class GenericCRUDRepository<T> : ICRUDRepository<T>
    {
        protected abstract string TableName { get; }
        protected virtual string PrimaryKey => "Id";

        private readonly IConfiguration _configuration;

        public GenericCRUDRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddAsync(T entity)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                var insertQuery = GenerateInsertQuery();
                return await connection.ExecuteAsync(insertQuery, entity);
            }
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                return connection.QueryAsync<T>($"SELECT * FROM {TableName}");
            }
        }

        public async Task<T> GetAsync<Tid>(Tid id)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                var result = await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {TableName} WHERE {PrimaryKey}=@Id", new { Id = id });
                if (result == null)
                {
                    throw new KeyNotFoundException($"{TableName} with id {id} could not be found.");
                }
                return result;
            }
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {TableName}(");
            var properties = GenerateListOfProperties(Properties);
            foreach (var property in properties)
            {
                insertQuery.Append($"{property},");
            }

            insertQuery.Remove(insertQuery.Length - 1, 1).Append(") VALUES (");
            foreach (var property in properties)
            {
                insertQuery.Append($"@{property},");
            }

            insertQuery.Remove(insertQuery.Length - 1, 1).Append(");");
            return insertQuery.ToString();
        }
        
        private static IEnumerable<PropertyInfo> Properties => typeof(T).GetProperties();

        private IEnumerable<string> GenerateListOfProperties(IEnumerable<PropertyInfo> properties)
        {
            return properties.Where(property => {
                var attributes = property.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore") && !property.Name.Equals("Id");
            }).Select(property => property.Name);
        }
    }
}
