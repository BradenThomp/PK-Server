using Application.Features.Tracking.Dtos;
using System;

namespace Application.Features.Map.Dtos
{
    public record MapPlotPointDto(Guid RentalId, string CustomerName, String SpeakerSerialNumber, string SpeakerModel, TrackerDto Tracker);
}
