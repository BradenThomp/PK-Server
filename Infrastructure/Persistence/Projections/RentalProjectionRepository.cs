using Dapper;
using Domain.Aggregates;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Projections
{
    class RentalProjectionRepository : IProjectionWriter<Rental>
    {
        private readonly IConfiguration _configuration;
        public RentalProjectionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveProjection(Rental aggregate)
        {
            var projection = aggregate.CreateProjection();
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                return await connection.ExecuteAsync("INSERT INTO rental_projection(CustomerId, RentalDate, ExpectedReturnDate, VenueId, Id) " +
                    "VALUES (@CustomerId, @RentalDate, @ExpectedReturnDate, @VenueId, @Id) " +
                    "ON DUPLICATE KEY UPDATE CustomerId=@CustomerId, RentalDate=@RentalDate, ExpectedReturnDate=@ExpectedReturnDate, VenueId=@VenueId, Id=@Id;", projection);
            }
        }
    }
}
