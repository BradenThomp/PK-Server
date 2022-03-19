using Domain.Common.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Models
{
    /// <summary>
    /// Represents an email that recieves application notifications.
    /// </summary>
    public class NotificationEmail
    {
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                var r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (!r.IsMatch(value))
                {
                    throw new DomainValidationException($"Email number {value} is not valid.");
                }
                _email = value;
            }
        }

        public NotificationEmail() { }
        public NotificationEmail(string email)
        {
            Email = email;
        }
    }
}
