using Application.Common.Repository;
using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class TrackerRepository : BaseCRUDRepository<Tracker>, ITrackerRepository
    {
        public TrackerRepository(IConfiguration configuration) : base(configuration) { }

        public override async Task AddAsync(Tracker entity)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                var insertQuery = $"INSERT INTO location(Id, Longitude, Latitude) VALUES(@Id, @Longitude, @Latitude)";
                await connection.ExecuteAsync(insertQuery, new { Id=entity.Location.Id, Longitude=entity.Location.Longitude, Latitude=entity.Location.Latitude });
                
                insertQuery = $"INSERT INTO tracker(HardwareId, LastUpdate, LocationId) VALUES(@HardwareId, @LastUpdate, @LocationId)";
                await connection.ExecuteAsync(insertQuery, new { HardwareId=entity.HardwareId, LastUpdate=entity.LastUpdate, LocationId=entity.Location.Id });
            }
        }

        public override Task DeleteAsync(Tracker entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<Tracker>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                return await connection.QueryAsync<Tracker, Location, Tracker>(
                    $"SELECT * " +
                    $"FROM tracker t " +
                    $"INNER JOIN location l ON t.LocationId = l.Id", (tracker, location) => {
                        tracker.Location = location;
                        return tracker;
                    }, splitOn: "LocationId");
            }
        }

        public override Task<Tracker> GetAsync<Tid>(Tid id)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(Tracker entity)
        {
            throw new NotImplementedException();
        }
    }
}
