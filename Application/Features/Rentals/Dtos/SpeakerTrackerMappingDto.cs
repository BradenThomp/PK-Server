namespace Application.Features.Rentals.Dtos
{
    /// <summary>
    /// Data transfer object for assigning a tracker to a speaker.
    /// </summary>
    public record SpeakerTrackerMappingDto(string SpeakerSerialNumber, string TrackerHardwareId);
}
