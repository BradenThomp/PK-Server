using Domain.Common.Models;
using System;

namespace Domain.Models
{
    public class Speaker : IModel
    {
        public Guid Id { get; init; }

        public string SerialNumber { get; init; }

        public string Model { get; init; }

        public Tracker Tracker { get; set; }
    }
}
