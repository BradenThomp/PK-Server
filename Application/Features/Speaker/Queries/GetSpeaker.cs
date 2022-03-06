using Application.Common.Repository;
using Application.Features.Speaker.Dtos;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Speaker.Queries
{
    public record GetSpeakerQuery(string SerialNumber) : IRequest<SpeakerDto>;

    public class GetSpeakerQueryQueryHandler : IRequestHandler<GetSpeakerQuery, SpeakerDto>
    {
        private readonly ISpeakerRepository _repository;
        private readonly IMapper _mapper;

        public GetSpeakerQueryQueryHandler(ISpeakerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a speaker with the given serial number.
        /// </summary>
        /// <param name="request">The query wrapper.</param>
        /// <param name="cancellationToken">Token to cancel the task.</param>
        /// <returns>The requested speaker.</returns>
        public async Task<SpeakerDto> Handle(GetSpeakerQuery request, CancellationToken cancellationToken)
        {
            var s = await _repository.GetAsync(request.SerialNumber);
            return _mapper.Map<Domain.Models.Speaker, SpeakerDto>(s);
        }
    }
}
