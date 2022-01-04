using Application.Common.Repository;
using Domain.Aggregates;
using Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tracking
{
    public record UpdateTrackerLocationCommand(double Longitude, double Latitude, string MACAddress, DateTime TimeOfUpdate) : IRequest;

    public class UpdateTrackerLocationCommandHandler : IRequestHandler<UpdateTrackerLocationCommand>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateTrackerLocationCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(UpdateTrackerLocationCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _eventRepository.GetByIdAsync<Tracker>(request.MACAddress) as Tracker;
            aggregate.UpdateLocation(new Location(request.Longitude, request.Latitude), request.TimeOfUpdate);
            await _eventRepository.SaveAsync(aggregate);
            return Unit.Value;
        }
    }
}
