using Application.Features.Tracking.Dtos;

namespace Application.Features.Speaker.Dtos
{
    public record SpeakerDto(string SerialNumber, string Model, TrackerDto Tracker);
}
