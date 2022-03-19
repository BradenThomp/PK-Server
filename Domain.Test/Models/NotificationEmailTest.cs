using Domain.Common.Exceptions;
using Domain.Models;
using NUnit.Framework;

namespace Domain.Test.Models
{
    public class NotificationEmailTest
    {
        [Test]
        public void ShouldConstructObject_ValidEmail()
        {
            NotificationEmail e = new NotificationEmail("bob@outlook.com");
            Assert.That(e.Email, Is.EqualTo("bob@outlook.com"));
        }

        [Test]
        public void ShouldThrowValidationException_InvalidEmail_Constructor()
        {
            Assert.Throws<DomainValidationException>(() => new NotificationEmail("boboutlook.com"));
        }
    }
}
