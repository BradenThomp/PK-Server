using Domain.Common.Models;
using System;

namespace Domain.Models
{
    public class Venue : IModel
    {
        public Location Cooridinates { get; set; }

        public Guid Id { get; set; }
    }
}
