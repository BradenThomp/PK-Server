using Application.Common.Repository;
using Application.Common.Services;
using FluentScheduler;
using System;

namespace Infrastructure.Emails
{
    /// <summary>
    /// A background job that can be scheduled to email subscribers if a rental has not been returned.
    /// </summary>
    public class OutstandingRentalsJob : IJob
    {
        private readonly IRentalRepository _rentalRepo;
        private readonly IEmailService _emailService;

        public OutstandingRentalsJob(IRentalRepository rentalRepo, IEmailService emailService)
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
                var subject = "A rental has not been returned.";
                var content = "Rental " + r.Id + " has outstanding speakers that have not been returned. The rental was expected to be returned by " + r.ExpectedReturnDate + ".  The speakers that have not been returned are:\n\n";
                foreach(var speaker in r.RentedSpeakers)
                {
                    content += "Speaker: " + speaker.SerialNumber + "\n";
                }
                if ( DateTime.Compare(today, r.ExpectedReturnDate) > 0 && r.DateReturned == null) {
                    _emailService.MailAll(subject, content);
                }
            }
           
        }
    }
}
