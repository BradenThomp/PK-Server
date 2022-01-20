using Domain.Models;
using System;

namespace Domain.Projections
{
    public class TrackerProjection : IProjection
    {
        public string MACAddress { get; set; }
        
        public double Longitude { get; set; }
        
        public double Latitude { get; set; }

        public DateTime LastUpdate { get; set; } 
        
        public Speaker Speaker { get; set; }

        public TrackerProjection() { }
        public TrackerProjection(string macAddress, double longitude, double latitude, DateTime lastUpdate, Speaker speaker)
        {
            MACAddress = macAddress;
            Longitude = longitude;
            Latitude = latitude;
            LastUpdate = lastUpdate;
            Speaker = speaker;
        }
    }
}
