using Application.Common.Repository;
using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class RentalRepository : BaseCRUDRepository<Rental>, IRentalRepository
    {
        public RentalRepository(IConfiguration configuration) : base(configuration) { }

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
                    var updateQuery = $"UPDATE speaker SET TrackerId=@TrackerId, RentalId=@RentalId WHERE SerialNumber=@SerialNumber";
                    await connection.ExecuteAsync(updateQuery, new { SerialNumber = speaker.SerialNumber, RentalId=entity.Id, TrackerId = speaker.Tracker?.HardwareId });
                }
            }
        }

        public override Task DeleteAsync(Rental entity)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<IEnumerable<Rental>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                var rentals = await connection.QueryAsync<Rental, Customer, Venue, Rental>(
                    $"SELECT *" +
                    $"FROM rental r " +
                    $"INNER JOIN customer c ON c.Id = r.CustomerId " +
                    $"INNER JOIN venue v ON v.Id = r.DestinationId", 
                    (rental, customer, destination) =>
                    {
                        rental.Customer = customer;
                        rental.Destination = destination;
                        return rental;
                    });

                foreach (var rental in rentals)
                {
                    rental.RentedSpeakers = (await connection.QueryAsync<Speaker, Tracker, Location, Speaker>(
                        $"SELECT * " +
                        $"FROM speaker s " +
                        $"LEFT JOIN tracker t ON s.TrackerId = t.HardwareId " +
                        $"LEFT JOIN location l ON t.LocationId = l.Id " +
                        $"WHERE s.RentalId=@RentalId", (speaker, tracker, location) => {
                            if(tracker.HardwareId is not null)
                            {
                                tracker.Location = location;
                                speaker.Tracker = tracker;
                            }
                            speaker.RentalId = rental.Id;
                            return speaker;
                        }, new { RentalId = rental.Id }, splitOn: "TrackerId,LocationId")).AsList<Speaker>();

                    rental.ReturnedSpeakers = (await connection.QueryAsync<ReturnedSpeaker>(
                        $"SELECT * " +
                        $"FROM returned_speaker s " +
                        $"WHERE s.RentalId=@RentalId", new { RentalId = rental.Id })).AsList();
                }
                return rentals;
            }
        }

        public override async Task<Rental> GetAsync<Tid>(Tid id)
        {
            var all = await GetAllAsync();
            return all.First(x => x.Id == id as Guid?);
        }

        public override async Task UpdateAsync(Rental entity)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                var updateQuery = $"UPDATE rental SET DateReturned=@DateReturned WHERE Id=@Id";
                await connection.ExecuteAsync(updateQuery, new { DateReturned = entity.DateReturned, Id = entity.Id });

                foreach(var returnedRental in entity.ReturnedSpeakers)
                {
                    updateQuery = $"UPDATE speaker SET RentalId=@RentalId, TrackerId=@TrackerId  WHERE SerialNumber=@SerialNumber";
                    await connection.ExecuteAsync(updateQuery, new { RentalId = (Guid?)null, TrackerId=(Guid?)null, SerialNumber = returnedRental.SerialNumber });

                    var insertQuery = $"INSERT INTO returned_speaker(SerialNumber, Model, RentalId, DateReturned) VALUES(@SerialNumber, @Model, @RentalId, @DateReturned)";
                    await connection.ExecuteAsync(insertQuery, new { SerialNumber = returnedRental.SerialNumber, Model = returnedRental.Model, RentalId = returnedRental.RentalId, DateReturned = returnedRental.DateReturned});
                }
            }
        }
    }
}
