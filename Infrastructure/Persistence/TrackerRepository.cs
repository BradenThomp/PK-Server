﻿using Application.Common.Repository;
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

        public override async Task<Tracker> GetAsync<Tid>(Tid id)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                return (await connection.QueryAsync<Tracker, Location, Tracker>(
                    $"SELECT * " +
                    $"FROM tracker t " +
                    $"INNER JOIN location l ON t.LocationId = l.Id " +
                    $"WHERE t.HardwareId = @HardwareId", (tracker, location) => {
                        tracker.Location = location;
                        return tracker;
                    }, new { HardwareId=id }, splitOn: "LocationId")).Single();
            }
        }

        public override async Task UpdateAsync(Tracker entity)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                var updateQuery = $"UPDATE location SET Longitude=@Longitude, Latitude=@Latitude WHERE Id=@Id";
                await connection.ExecuteAsync(updateQuery, new { Id = entity.Location.Id, Longitude = entity.Location.Longitude, Latitude = entity.Location.Latitude });

                updateQuery = $"UPDATE tracker SET LastUpdate=@LastUpdate, LocationId=@LocationId WHERE HardwareId=@HardwareId";
                await connection.ExecuteAsync(updateQuery, new { HardwareId = entity.HardwareId, LastUpdate = entity.LastUpdate, LocationId = entity.Location.Id });
            }
        }
    }
}
