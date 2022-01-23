using Domain.Common.Models;
using System;

namespace Domain.Models
{
    public class Speaker : IModel
    {
        public string SerialNumber { get; init; }

        public string Model { get; init; }

        public Tracker Tracker { get; set; }

        public Speaker() { }

        public Speaker(string serialNumber, string model)
        {
            SerialNumber = serialNumber;
            Model = model;
        }
    }
}
