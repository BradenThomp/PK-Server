using Application.Common.Repository;
using Application.Common.Services;
using FluentScheduler;

namespace Infrastructure.Emails
{
    public class EmailJobRegistry : Registry
    {
        public EmailJobRegistry(IRentalRepository rentalRepo, IEmailService emailService)
        {
            Schedule(new OutstandingRentalsJob(rentalRepo, emailService)).ToRunEvery(1).Days();
            Schedule(new UnarrivedRentalsJob(rentalRepo, emailService)).ToRunEvery(10).Seconds();
        }
    }
}
