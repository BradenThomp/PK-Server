using Application.Common.Repository;
using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class SpeakerRepository : GenericCRUDRepository<Speaker>, ISpeakerRepository
    {
        protected override string TableName => "speaker";

        protected override string PrimaryKey => "SerialNumber";

        public SpeakerRepository(IConfiguration configuration) : base(configuration) { }

        public new async Task UpdateAsync(Speaker entity)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                await connection.ExecuteAsync("UPDATE speaker SET Model=@Model, TrackerId=@TrackerId", new { entity.Model, entity.TrackerId});
            }
        }
    }
}
