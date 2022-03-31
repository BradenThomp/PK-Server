﻿using Application.Common.Repository;
using Application.Features.Tracking.Dtos;
using AutoMapper;
using Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking.Queries
{
    public record GetAvailableTrackersQuery() : IRequest<IEnumerable<TrackerDto>>;

    public class GetAvailableTrackersQueryHandler : IRequestHandler<GetAvailableTrackersQuery, IEnumerable<TrackerDto>>
    {
        private readonly ITrackerRepository _repo;
        private readonly IMapper _mapper;

        public GetAvailableTrackersQueryHandler(ITrackerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all available trackers in the system (trackers not assigned to a rental).
        /// </summary>
        /// <param name="request">The query wrapper.</param>
        /// <param name="cancellationToken">Token to cancel the task.</param>
        /// <returns>A list of all trackers.</returns>
        public async Task<IEnumerable<TrackerDto>> Handle(GetAvailableTrackersQuery request, CancellationToken cancellationToken)
        {
            var trackers = await _repo.GetAllAsync();
            trackers = trackers.Where(t => t.SpeakerSerialNumber == null);
            return _mapper.Map<IEnumerable<Tracker>, IEnumerable<TrackerDto>>(trackers);
        }
    }
}