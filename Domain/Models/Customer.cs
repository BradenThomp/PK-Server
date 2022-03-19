using Domain.Common.Exceptions;
using Domain.Common.Models;
using System;
using System.Text.RegularExpressions;

namespace Domain.Models
{
    /// <summary>
    /// Represents a customer that has rented speakers.
    /// </summary>
    public class Customer : IModel
    {
        public Guid Id { get; init; }

        public string Name { get; set; }

        private string _phone;
        public string Phone 
        { 
            get => _phone; 
            set 
            {
                var r = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
                if (!r.IsMatch(value))
                {
                    throw new DomainValidationException($"Phone number {value} is not valid.");
                }
                _phone = value;
            } 
        }

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

        public Customer() { }

        public Customer(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Id = Guid.NewGuid();
        }
    }
}
