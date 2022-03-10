using Application.Common.Repository;
using FluentScheduler;

namespace Infrastructure.Emails
{
    public class EmailJobRegistry : Registry
    {
        public EmailJobRegistry(IRentalRepository rentalRepo)
        {
            Schedule(new OutstandingRentalsJob(rentalRepo)).ToRunEvery(10).Seconds();
        }
    }
}
