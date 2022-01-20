using Application.Common.Repository;
using Dapper;
using Domain.Aggregates;
using Domain.Models;
using Domain.Projections;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Projections
{
    public class TrackerProjectionRepository : IProjectionWriter<Tracker>, ITrackerProjectionRepository
    {
        private readonly IConfiguration _configuration;
        public TrackerProjectionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<TrackerProjection>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                return await connection.QueryAsync<TrackerProjection, Speaker, TrackerProjection>(
                    $"SELECT * " +
                    $"FROM tracker_projection t " +
                    $"LEFT JOIN speaker s ON t.SpeakerSerialNumber = s.SerialNumber", (tracker, speaker) => {
                        tracker.Speaker = speaker;
                        return tracker;
                    }, splitOn: "SpeakerSerialNumber");
            }
        }

        public Task<TrackerProjection> GetAsync<Tid>(Tid id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> SaveProjection(Tracker aggregate)
        {
            var projection = aggregate.CreateProjection() as TrackerProjection;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                return await connection.ExecuteAsync(
                    "INSERT INTO tracker_projection(MACAddress, Longitude, Latitude, LastUpdate, SpeakerSerialNumber) " +
                    "VALUES (@MACAddress, @Longitude, @Latitude, @LastUpdate, @SerialNumber) " +
                    "ON DUPLICATE KEY UPDATE Longitude=@Longitude, Latitude=@Latitude, LastUpdate=@LastUpdate, SpeakerSerialNumber=@SerialNumber;", 
                    new { projection.MACAddress, projection.Longitude, projection.Latitude, projection.LastUpdate, projection.Speaker?.SerialNumber});
            }
        }
    }
}
