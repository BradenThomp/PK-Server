using Domain.Common.Models;

namespace Domain.Models
{
    public class Speaker : IModel
    {
        public string SerialNumber { get; init; }

        public string Model { get; init; }

        public Speaker(string serialNumber, string model)
        {
            SerialNumber = serialNumber;
            Model = model;
        }
    }
}
