using Application.Common.Repository;
using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Infrastructure.Persistence
{
    public class SpeakerRepository : BaseCRUDRepository<Speaker>, ISpeakerRepository
    {

        public SpeakerRepository(IConfiguration configuration) : base(configuration) { }

        public override async Task AddAsync(Speaker entity)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                var insertQuery = $"INSERT INTO speaker(SerialNumber, Model) VALUES(@SerialNumber, @Model)";
                await connection.ExecuteAsync(insertQuery, new { SerialNumber = entity.SerialNumber, Model = entity.Model });
            }
        }

        public override Task DeleteAsync(Speaker entity)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<IEnumerable<Speaker>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                return await connection.QueryAsync<Speaker, Tracker, Location, Speaker>(
                    $"SELECT * " +
                    $"FROM speaker s " +
                    $"LEFT JOIN tracker t ON s.TrackerId = t.HardwareId " +
                    $"LEFT JOIN location l ON t.LocationId = l.Id", (speaker, tracker, location) => {
                        if(tracker.HardwareId is not null)
                        {
                            tracker.Location = location;
                            speaker.Tracker = tracker;
                        }
                        return speaker;
                    }, splitOn: "TrackerId,LocationId");
            }
        }

        public override async Task<Speaker> GetAsync<Tid>(Tid id)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                return (await connection.QueryAsync<Speaker, Tracker, Location, Speaker>(
                    $"SELECT * " +
                    $"FROM speaker s " +
                    $"LEFT JOIN tracker t ON s.TrackerId = t.HardwareId " +
                    $"LEFT JOIN location l ON t.LocationId = l.Id " +
                    $"WHERE s.SerialNumber = @SerialNumber", (speaker, tracker, location) => {
                        if (tracker.HardwareId is not null)
                        {
                            tracker.Location = location;
                            speaker.Tracker = tracker;
                        }
                        return speaker;
                    }, new { SerialNumber = id}, splitOn: "TrackerId,LocationId")).Single();
            }
        }

        public override async Task UpdateAsync(Speaker entity)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                var updateQuery = $"UPDATE speaker SET TrackerId=@TrackerId WHERE SerialNumber=@SerialNumber";
                await connection.ExecuteAsync(updateQuery, new { SerialNumber = entity.SerialNumber, TrackerId = entity.Tracker?.HardwareId });
            }
        }
    }
}
