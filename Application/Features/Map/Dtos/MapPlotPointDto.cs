using Application.Features.Tracking.Dtos;
using System;

namespace Application.Features.Map.Dtos
{
    /// <summary>
    /// A Data Transfer Object for displaying map plot points in the user interface.
    /// </summary>
    public record MapPlotPointDto(Guid RentalId, string CustomerName, String SpeakerSerialNumber, string SpeakerModel, TrackerDto Tracker);
}
