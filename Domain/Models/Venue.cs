using Domain.Common.Models;

namespace Domain.Models
{
    public class Venue : IModel
    {
        public Location Cooridinates { get; set; }
    }
}
