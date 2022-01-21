using Domain.Common.Models;
using System;

namespace Domain.Models
{
    public class Tracker : IModel
    {
        public string HardwareId { get; init; }

        public DateTime LastUpdate { get; set; }

        public Location Location { get; set; }
    }
}
