using System.Threading.Tasks;

namespace Application.Common.Services
{
    /// <summary>
    /// A service for sending emails.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email to everyone in the system.
        /// </summary>
        /// <param name="subject">The email subject line.</param>
        /// <param name="body">The email body.</param>
        /// <returns>A task so it can be exectued async</returns>
        Task MailAll(string subject, string body);
    }
}
