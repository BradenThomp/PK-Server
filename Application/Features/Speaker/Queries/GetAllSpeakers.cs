using Application.Common.Repository;
using Application.Features.Speaker.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Features.Speaker.Queries
{
    public record GetAllSpeakersQuery() : IRequest<IEnumerable<SpeakerDto>>;

    public class GetAllSpeakersQueryHandler : IRequestHandler<GetAllSpeakersQuery, IEnumerable<SpeakerDto>>
    {
        private readonly ISpeakerRepository _repository;

        public GetAllSpeakersQueryHandler(ISpeakerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SpeakerDto>> Handle(GetAllSpeakersQuery request, CancellationToken cancellationToken)
        {
            var speakers = await _repository.GetAllAsync();
            return speakers.Select(s => new SpeakerDto(s.Model, s.SerialNumber));
        }
    }
}
