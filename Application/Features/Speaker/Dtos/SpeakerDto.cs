using Domain.Aggregates;

namespace Application.Features.Speaker.Dtos
{
    public class SpeakerDto
    {
        public string Model { get; }

        public string SerialNumber { get; }

        public SpeakerDto(string model, string serialNumber)
        {
            Model = model;
            SerialNumber = serialNumber;
        }
    }
}
