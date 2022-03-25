using Application.Common.Repository;
using Application.Common.Services;
using FluentScheduler;
using System;
using System.Linq;

namespace Infrastructure.Emails
{
    class UnarrivedRentalsJob : IJob
    {
        private readonly IRentalRepository _rentalRepo;
        private readonly IEmailService _emailService;

        public UnarrivedRentalsJob(IRentalRepository rentalRepo, IEmailService emailService)
        {
            _rentalRepo = rentalRepo;
            _emailService = emailService;
        }
        public void Execute()
        {
            var rentals = _rentalRepo.GetAllAsync().GetAwaiter().GetResult();
            var today = DateTime.Now;
            foreach (Domain.Models.Rental r in rentals)
            {
                if (r.RentedSpeakers.Count(s => !s.ReachedDestination) == 0)
                {
                    continue;
                }
                var subject = "A rental has not arrived at the destination.";
                var content = $"Rental {r.Id} has speakers that have not arrived at the expected destination. The rental was expected to be arrive at {r.Destination.Address}, {r.Destination.City} by {r.ArrivalDate}.  The speakers that have not reached the destination are:\n\n";
                foreach (var speaker in r.RentedSpeakers)
                {
                    if (!speaker.ReachedDestination)
                    {
                        content += "Speaker: " + speaker.SerialNumber + "\n";
                    }
                }
                if (DateTime.Compare(today, r.ArrivalDate) > 0 && !r.Destination.Cooridinates.IsPlaceHolder())
                {
                    // We don't want to send an email if the coordinates could not be resolved and were instead populated with placeholders.
                    _emailService.MailAll(subject, content);
                }
            }
        }
    }
}
