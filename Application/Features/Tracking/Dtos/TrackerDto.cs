using System;

namespace Application.Features.Tracking.Dtos
{
    public record TrackerDto(string HardwareId, DateTime LastUpdate, LocationDto Location);
}
