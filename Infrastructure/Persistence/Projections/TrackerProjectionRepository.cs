using Application.Common.Repository;
using Dapper;
using Domain.Aggregates;
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
                return await connection.QueryAsync<TrackerProjection>($"SELECT * FROM tracker_projection");
            }
        }

        public Task<TrackerProjection> GetAsync<Tid>(Tid id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> SaveProjection(Tracker aggregate)
        {
            var projection = aggregate.CreateProjection();
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                return await connection.ExecuteAsync("INSERT INTO tracker_projection(MACAddress, Longitude, Latitude, LastUpdate) VALUES (@MACAddress, @Longitude, @Latitude, @LastUpdate) ON DUPLICATE KEY UPDATE Longitude=@Longitude, Latitude=@Latitude, LastUpdate=@LastUpdate;", projection);
            }
        }
    }
}
