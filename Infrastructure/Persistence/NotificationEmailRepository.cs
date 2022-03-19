using Application.Common.Repository;
using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    /// <inheritdoc/>
    public class NotificationEmailRepository : BaseCRUDRepository<NotificationEmail>, INotificationEmailRepository
    {
        public NotificationEmailRepository(IConfiguration configuration) : base(configuration) { }

        /// <inheritdoc/>
        public override async Task AddAsync(NotificationEmail entity)
        {
            await Commit(async con =>
            {
                var insertQuery = $"INSERT INTO notification_email(Email) VALUES(@Email)";
                await con.ExecuteAsync(insertQuery, new { Email = entity.Email});
            });
        }

        /// <inheritdoc/>
        public override Task DeleteAsync(NotificationEmail entity)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<NotificationEmail>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                return await connection.QueryAsync<NotificationEmail>(
                    $"SELECT * " +
                    $"FROM notification_email t");
            }
        }

        /// <inheritdoc/>
        public override Task<NotificationEmail> GetAsync<Tid>(Tid id)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task UpdateAsync(NotificationEmail entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
