using System;

namespace Domain.Projections
{
    public record TrackerProjection(string MACAddress, double Longitude, double Latitude, DateTime LastUpdate, string SpeakerSerialNumber) : IProjection;
}
