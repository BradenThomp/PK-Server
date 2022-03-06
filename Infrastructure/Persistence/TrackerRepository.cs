using Application.Common.Repository;
using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    /// <inheritdoc/>
    public class TrackerRepository : BaseCRUDRepository<Tracker>, ITrackerRepository
    {
        public TrackerRepository(IConfiguration configuration) : base(configuration) { }

        /// <inheritdoc/>
        public override async Task AddAsync(Tracker entity)
        {
            await Commit(async con =>
            {
                var insertQuery = $"INSERT INTO location(Id, Longitude, Latitude) VALUES(@Id, @Longitude, @Latitude)";
                await con.ExecuteAsync(insertQuery, new { Id = entity.Location.Id, Longitude = entity.Location.Longitude, Latitude = entity.Location.Latitude });

                insertQuery = $"INSERT INTO tracker(HardwareId, LastUpdate, LocationId) VALUES(@HardwareId, @LastUpdate, @LocationId)";
                await con.ExecuteAsync(insertQuery, new { HardwareId = entity.HardwareId, LastUpdate = entity.LastUpdate, LocationId = entity.Location.Id });
            });
        }

        /// <inheritdoc/>
        public override Task DeleteAsync(Tracker entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Tracker>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                return await connection.QueryAsync<Tracker, Location, Tracker>(
                    $"SELECT * " +
                    $"FROM tracker t " +
                    $"INNER JOIN location l ON l.Id = t.LocationId", (tracker, location) => {
                        tracker.Location = location;
                        return tracker;
                    }, splitOn: "LocationId");
            }
        }

        /// <inheritdoc/>
        public override async Task<Tracker> GetAsync<Tid>(Tid id)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                return (await connection.QueryAsync<Tracker, Location, Tracker>(
                    $"SELECT * " +
                    $"FROM tracker t " +
                    $"INNER JOIN location l ON l.Id = t.LocationId " +
                    $"WHERE t.HardwareId = @HardwareId", (tracker, location) => {
                        tracker.Location = location;
                        return tracker;
                    }, new { HardwareId=id }, splitOn: "LocationId")).Single();
            }
        }

        /// <inheritdoc/>
        public override async Task UpdateAsync(Tracker entity)
        {
            await Commit(async con =>
            {
                var updateQuery = $"UPDATE location SET Longitude=@Longitude, Latitude=@Latitude WHERE Id=@Id";
                await con.ExecuteAsync(updateQuery, new { Id = entity.Location.Id, Longitude = entity.Location.Longitude, Latitude = entity.Location.Latitude });

                updateQuery = $"UPDATE tracker SET LastUpdate=@LastUpdate, LocationId=@LocationId WHERE HardwareId=@HardwareId";
                await con.ExecuteAsync(updateQuery, new { HardwareId = entity.HardwareId, LastUpdate = entity.LastUpdate, LocationId = entity.Location.Id });
            });
        }
    }
}
