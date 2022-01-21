using Domain.Common.Models;
using System;

namespace Domain.Models
{
    public class Customer : IModel
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Guid Id { get; init; }
    }
}
