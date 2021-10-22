using Application.Common.Repository;
using Domain.Features.Tracking;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking
{
    public record UpdateTrackerLocationCommand(double Longitude, double Latitude, Guid TrackerId, DateTime TimeOfUpdate) : IRequest;

    public class UpdateTrackerLocationCommandHandler : IRequestHandler<UpdateTrackerLocationCommand>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateTrackerLocationCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(UpdateTrackerLocationCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _eventRepository.GetByIdAsync<Tracker>(request.TrackerId) as Tracker;
            aggregate.UpdateLocation(request.Longitude, request.Latitude, request.TimeOfUpdate);
            await _eventRepository.SaveAsync(aggregate);
            return Unit.Value;
        }
    }
}
