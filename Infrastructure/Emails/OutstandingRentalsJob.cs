using Application.Common.Repository;
using FluentScheduler;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Emails
{
    /// <summary>
    /// A background job that can be scheduled to email subscribers if a rental has not been returned.
    /// </summary>
    public class OutstandingRentalsJob : IJob
    {
        private readonly IRentalRepository _rentalRepo;
        private readonly INotificationEmailRepository _emailRepository;
        protected readonly IConfiguration _configuration;

        public OutstandingRentalsJob(IRentalRepository rentalRepo, INotificationEmailRepository emailRepo)
        {
            _rentalRepo = rentalRepo;
            _emailRepository = emailRepo;
        }

        public void SendMail(string address, string subject, string body)
        {


            var fromAddress = new MailAddress("capstone765@gmail.com", "PK Sound Rental Tracker");
            var toAddress = new MailAddress(address, "Subscriber");
            const string fromPassword = "Calgary123@";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
        public void Execute()
        {
            var rentals = _rentalRepo.GetAllAsync().GetAwaiter().GetResult();
            var emails = _emailRepository.GetAllAsync().GetAwaiter().GetResult();
            var today = DateTime.Now;
            foreach (Domain.Models.Rental r in rentals)
            {
                var subject = "A rental has not been returned.";
                var content = "Rental " + r.Id + " has outstanding speakers that have not been returned. The rental was expected to be returned by " + r.ExpectedReturnDate + ".  The speakers that have not been returned are:\n\n";
                foreach(var speaker in r.RentedSpeakers)
                {
                    content += "Speaker: " + speaker.SerialNumber + "\n";
                }
                var res = DateTime.Compare(today, r.ExpectedReturnDate);
                if ( DateTime.Compare(today, r.ExpectedReturnDate) > 0 && r.DateReturned == null) {
                    foreach(var email in emails)
                    {
                        SendMail(email.Email, subject, content);
                    }
                }
            }
           
        }
    }
}
