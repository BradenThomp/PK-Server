﻿using Application.Common.Repository;
using Application.Features.Speaker.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Features.Speaker.Queries
{
    public record GetAllSpeakersQuery() : IRequest<IEnumerable<SpeakerDto>>;

    public class GetAllSpeakersQueryHandler : IRequestHandler<GetAllSpeakersQuery, IEnumerable<SpeakerDto>>
    {
        private readonly ISpeakerRepository _repository;
        private readonly IMapper _mapper;

        public GetAllSpeakersQueryHandler(ISpeakerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SpeakerDto>> Handle(GetAllSpeakersQuery request, CancellationToken cancellationToken)
        {
            var speakers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<Domain.Models.Speaker>, IEnumerable<SpeakerDto>>(speakers);
        }
    }
}
