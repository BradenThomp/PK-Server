using System;

namespace Application.Features.Tracking.Dtos
{
    /// <summary>
    /// A data tranfer object for trackers.
    /// </summary>
    public record TrackerDto(string HardwareId, DateTime LastUpdate, LocationDto Location);
}
