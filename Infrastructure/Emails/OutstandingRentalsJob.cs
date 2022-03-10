using Application.Common.Repository;
using FluentScheduler;
using System;

namespace Infrastructure.Emails
{
    public class OutstandingRentalsJob : IJob
    {
        private readonly IRentalRepository _repo;

        public OutstandingRentalsJob(IRentalRepository repo)
        {
            _repo = repo;
        }

        public void Execute()
        {
            var rentals = _repo.GetAllAsync().GetAwaiter().GetResult();
            Console.WriteLine("Here");
            // Check each rental to see if it has exceeded the return date.
            // If rental is not returned in time, send an email.
        }
    }
}
