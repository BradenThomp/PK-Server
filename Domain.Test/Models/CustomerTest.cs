using Domain.Common.Exceptions;
using Domain.Models;
using NUnit.Framework;

namespace Domain.Test.Models
{
    public class CustomerTest
    {
        [Test]
        public void ShouldConstructObject_ValidEmailAndPhone()
        {
            Customer c = new Customer("Bob", "204-492-4321", "bob@outlook.com");
            Assert.That(c.Name, Is.EqualTo("Bob"));
            Assert.That(c.Phone, Is.EqualTo("204-492-4321"));
            Assert.That(c.Email, Is.EqualTo("bob@outlook.com"));
        }

        [Test]
        public void ShouldThrowValidationException_InvalidPhoneNumber_Constructor()
        {
            Assert.Throws<DomainValidationException>(() => new Customer("Bob", "204-492-41", "bob@outlook.com"));
        }

        [Test]
        public void ShouldThrowValidationException_InvalidEmail_Constructor()
        {
            Assert.Throws<DomainValidationException>(() => new Customer("Bob", "204-492-41", "boboutlook.com"));
        }
    }
}
