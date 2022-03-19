using Application.Common.Repository;
using FluentScheduler;

namespace Infrastructure.Emails
{
    public class EmailJobRegistry : Registry
    {
        public EmailJobRegistry(IRentalRepository rentalRepo, INotificationEmailRepository emailRepo)
        {
            Schedule(new OutstandingRentalsJob(rentalRepo, emailRepo)).ToRunEvery(1).Days();
        }
    }
}
