using Application.Features.Tracking.Dtos;

namespace Application.Features.Speaker.Dtos
{
    /// <summary>
    /// A data transfer object for speakers.
    /// </summary>
    public record SpeakerDto(string SerialNumber, string Model, TrackerDto Tracker);
}
