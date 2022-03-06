using Application.Features.Rentals.Dtos;
using Application.Features.Speaker.Dtos;
using Application.Features.Tracking.Dtos;
using AutoMapper;
using Domain.Models;

namespace Application.Common.Mappings
{
    /// <summary>
    /// Creates mapping profiles from one object to another using AutoMapper.
    /// A mapping profile allows for users to automatically map objects by 
    /// injecting the <see cref="AutoMapper.IMapper"/> interface.  This keeps
    /// lines smaller and easier to read.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Location, LocationDto>();
            CreateMap<Tracker, TrackerDto>();
            CreateMap<Speaker, SpeakerDto>();
            CreateMap<Venue, VenueDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Rental, RentalDto>().ForMember(dest => dest.Speakers, opt => opt.MapFrom<RentalResolver>());
        }
    }
}
