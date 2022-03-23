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
    /// <inheritdoc/>
    public class RentalRepository : BaseCRUDRepository<Rental>, IRentalRepository
    {
        public RentalRepository(IConfiguration configuration) : base(configuration) { }

        /// <inheritdoc/>
        public override async Task AddAsync(Rental entity) 
        {
            await Commit(async (con) =>
            {
                var insertQuery = $"INSERT INTO customer(Id, Name, Phone, Email) VALUES(@Id, @Name, @Phone, @Email)";
                await con.ExecuteAsync(insertQuery, new
                {
                    Id = entity.Customer.Id,
                    Name = entity.Customer.Name,
                    Phone = entity.Customer.Phone,
                    Email = entity.Customer.Email
                });

                insertQuery = $"INSERT INTO location(Id, Longitude, Latitude) VALUES(@Id, @Longitude, @Latitude)";
                await con.ExecuteAsync(insertQuery, new
                {
                    Id = entity.Destination.Cooridinates.Id,
                    Longitude = entity.Destination.Cooridinates.Longitude,
                    Latitude = entity.Destination.Cooridinates.Latitude,
                });

                insertQuery = $"INSERT INTO venue(Id, Address, City, Province, PostalCode, LocationId) VALUES(@Id, @Address, @City, @Province, @PostalCode, @LocationId)";
                await con.ExecuteAsync(insertQuery, new
                {
                    Id = entity.Destination.Id,
                    Address = entity.Destination.Address,
                    City = entity.Destination.City,
                    Province = entity.Destination.Province,
                    PostalCode = entity.Destination.PostalCode,
                    LocationId = entity.Destination.Cooridinates.Id
                });

                insertQuery = $"INSERT INTO rental(Id, RentalDate, ExpectedReturnDate, CustomerId, DestinationId) VALUES(@Id, @RentalDate, @ExpectedReturnDate, @CustomerId, @DestinationId)";
                await con.ExecuteAsync(insertQuery, new
                {
                    Id = entity.Id,
                    RentalDate = entity.RentalDate,
                    ExpectedReturnDate = entity.ExpectedReturnDate,
                    CustomerId = entity.Customer.Id,
                    DestinationId = entity.Destination.Id,
                });

                foreach (var speaker in entity.RentedSpeakers)
                {
                    var updateQuery = $"UPDATE speaker SET TrackerId=@TrackerId, RentalId=@RentalId WHERE SerialNumber=@SerialNumber";
                    await con.ExecuteAsync(updateQuery, new { SerialNumber = speaker.SerialNumber, RentalId = entity.Id, TrackerId = speaker.Tracker?.HardwareId });

                    updateQuery = $"UPDATE tracker SET SpeakerSerialNumber=@SerialNumber WHERE HardwareId=@TrackerId";
                    await con.ExecuteAsync(updateQuery, new { SerialNumber = speaker.SerialNumber, TrackerId = speaker.Tracker?.HardwareId });
                }
            });
        }

        /// <inheritdoc/>
        public override Task DeleteAsync(Rental entity)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Rental>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase")))
            {
                var rentals = await connection.QueryAsync<Rental, Customer, Venue, Location, Rental>(
                    $"SELECT *" +
                    $"FROM rental r " +
                    $"INNER JOIN customer c ON c.Id = r.CustomerId " +
                    $"INNER JOIN venue v ON v.Id = r.DestinationId " +
                    $"INNER JOIN location l ON l.Id = v.LocationId", 
                    (rental, customer, destination, location) =>
                    {
                        rental.Customer = customer;
                        destination.Cooridinates = location;
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
                                tracker.SpeakerSerialNumber = speaker.SerialNumber;
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

        /// <inheritdoc/>
        public override async Task<Rental> GetAsync<Tid>(Tid id)
        {
            var all = await GetAllAsync();
            return all.First(x => x.Id == id as Guid?);
        }

        /// <inheritdoc/>
        public override async Task UpdateAsync(Rental entity)
        {
            await Commit(async con =>
            {
                var updateQuery = $"UPDATE rental SET DateReturned=@DateReturned WHERE Id=@Id";
                await con.ExecuteAsync(updateQuery, new { DateReturned = entity.DateReturned, Id = entity.Id });

                foreach (var returnedRental in entity.ReturnedSpeakers)
                {
                    updateQuery = $"UPDATE speaker SET RentalId=@RentalId, TrackerId=@TrackerId  WHERE SerialNumber=@SerialNumber";
                    await con.ExecuteAsync(updateQuery, new { RentalId = (Guid?)null, TrackerId = (Guid?)null, SerialNumber = returnedRental.SerialNumber });

                    updateQuery = $"UPDATE tracker SET SpeakerSerialNumber=@NewSerialNumber WHERE SpeakerSerialNumber=@OldSerialNumber";
                    await con.ExecuteAsync(updateQuery, new { NewSerialNumber = (string)null, OldSerialNumber = returnedRental.SerialNumber });

                    var insertQuery = $"INSERT IGNORE INTO returned_speaker(SerialNumber, Model, RentalId, DateReturned) VALUES(@SerialNumber, @Model, @RentalId, @DateReturned)";
                    await con.ExecuteAsync(insertQuery, new { SerialNumber = returnedRental.SerialNumber, Model = returnedRental.Model, RentalId = returnedRental.RentalId, DateReturned = returnedRental.DateReturned });
                }
            });
        }
    }
}
