﻿using Domain.Common.Models;
using System;

namespace Domain.Models
{
    public class Customer : IModel
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Guid Id { get; init; }

        public Customer(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Id = Guid.NewGuid();
        }

        public Customer(string name, string phone, string email, Guid id)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Id = id;
        }
    }
}
