using Application.Features.Rentals.Dtos;
using Application.Features.Tracking.Dtos;
using AutoMapper;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Mappings
{
    public class RentalResolver : IValueResolver<Rental, RentalDto, IEnumerable<RentedSpeakerDto>>
    {
        public IEnumerable<RentedSpeakerDto> Resolve(Rental source, RentalDto destination, IEnumerable<RentedSpeakerDto> destMember, ResolutionContext context)
        {
            var speakers = source.RentedSpeakers.Select(s => new RentedSpeakerDto(s.SerialNumber, s.Model, new TrackerDto(s.Tracker.HardwareId, s.Tracker.LastUpdate, new LocationDto(s.Tracker.Location.Longitude, s.Tracker.Location.Latitude)), null));
            speakers = speakers.Concat(source.ReturnedSpeakers.Select(s => new RentedSpeakerDto(s.SerialNumber, s.Model, null, s.DateReturned)));
            return speakers;
        }
    }
}
