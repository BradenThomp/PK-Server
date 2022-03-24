using Application.Common.Repository;
using Application.Common.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Emails
{
    /// <inheritdoc/>
    public class EmailService : IEmailService
    {

        private readonly INotificationEmailRepository _emailRepository;

        public EmailService(INotificationEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        /// <inheritdoc/>
        public async Task MailAll(string subject, string body)
        {
            var emails = await _emailRepository.GetAllAsync();
            foreach(var email in emails)
            {
                SendMail(email.Email, subject, body);
            }
        }

        private void SendMail(string address, string subject, string body)
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
    }
}
