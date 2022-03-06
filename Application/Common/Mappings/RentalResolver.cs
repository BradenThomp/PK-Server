using Application.Features.Rentals.Dtos;
using Application.Features.Tracking.Dtos;
using AutoMapper;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Mappings
{
    /// <summary>
    /// Resolves a Rental to RentalDto mapping.
    /// </summary>
    public class RentalResolver : IValueResolver<Rental, RentalDto, IEnumerable<RentedSpeakerDto>>
    {
        /// <summary>
        /// Resolves the Rental to RentalDto mapping by combining the <see cref="Rental.RentedSpeakers"/>
        /// and <see cref="Rental.ReturnedSpeakers"/> into as singular list for <see cref="RentalDto.Speakers"/>.
        /// </summary>
        /// <param name="source">The <see cref="Rental"/> to map from.</param>
        /// <param name="destination">The <see cref="RentalDto"/> to map to.</param>
        /// <param name="destMember">The item to map to.</param>
        /// <param name="context">The resolution context.</param>
        /// <returns>The combined listed of <see cref="RentedSpeakerDto"/>.</returns>
        public IEnumerable<RentedSpeakerDto> Resolve(Rental source, RentalDto destination, IEnumerable<RentedSpeakerDto> destMember, ResolutionContext context)
        {
            var speakers = source.RentedSpeakers.Select(s => new RentedSpeakerDto(s.SerialNumber, s.Model, new TrackerDto(s.Tracker.HardwareId, s.Tracker.LastUpdate, new LocationDto(s.Tracker.Location.Longitude, s.Tracker.Location.Latitude)), null));
            speakers = speakers.Concat(source.ReturnedSpeakers.Select(s => new RentedSpeakerDto(s.SerialNumber, s.Model, null, s.DateReturned)));
            return speakers;
        }
    }
}
