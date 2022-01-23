using Application.Common.Repository;
using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class RentalRepository : BaseCRUDRepository<Rental>, IRentalRepository
    {
        private readonly ISpeakerRepository _speakerRepo;
        public RentalRepository(IConfiguration configuration, ISpeakerRepository speakerRepo) : base(configuration) 
        {
            _speakerRepo = speakerRepo;
        }

        public override async Task AddAsync(Rental entity)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                // TODO: do this in 1 transaction.
                var insertQuery = $"INSERT INTO customer(Id, Name, Phone, Email) VALUES(@Id, @Name, @Phone, @Email)";
                await connection.ExecuteAsync(insertQuery, new { 
                    Id = entity.Customer.Id, 
                    Name = entity.Customer.Name, 
                    Phone = entity.Customer.Phone,
                    Email = entity.Customer.Email
                });

                insertQuery = $"INSERT INTO venue(Id, Address, City, Province, PostalCode) VALUES(@Id, @Address, @City, @Province, @PostalCode)";
                await connection.ExecuteAsync(insertQuery, new
                {
                    Id = entity.Destination.Id,
                    Address = entity.Destination.Address,
                    City = entity.Destination.City,
                    Province = entity.Destination.Province,
                    PostalCode = entity.Destination.PostalCode
                });

                insertQuery = $"INSERT INTO rental(Id, RentalDate, ExpectedReturnDate, CustomerId, DestinationId) VALUES(@Id, @RentalDate, @ExpectedReturnDate, @CustomerId, @DestinationId)";
                await connection.ExecuteAsync(insertQuery, new
                {
                    Id = entity.Id,
                    RentalDate = entity.RentalDate,
                    ExpectedReturnDate = entity.ExpectedReturnDate,
                    CustomerId = entity.Customer.Id,
                    DestinationId = entity.Destination.Id,
                });

                foreach(var speaker in entity.RentedSpeakers)
                {
                    await _speakerRepo.UpdateAsync(speaker);
                }
            }
        }

        public override Task DeleteAsync(Rental entity)
        {
            throw new System.NotImplementedException();
        }

        public override Task<IEnumerable<Rental>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public override Task<Rental> GetAsync<Tid>(Tid id)
        {
            throw new System.NotImplementedException();
        }

        public override Task UpdateAsync(Rental entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
