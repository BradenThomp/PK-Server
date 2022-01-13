using Application.Common.Notifications;
using Application.Common.Repository;
using Application.Features.Tracking.Notification;
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
        private readonly INotificationService _notificationService;

        public UpdateTrackerLocationCommandHandler(IEventRepository eventRepository, INotificationService notificationService)
        {
            _eventRepository = eventRepository;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(UpdateTrackerLocationCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _eventRepository.GetByIdAsync<Tracker>(request.MACAddress) as Tracker;
            var newLocation = new Location(request.Longitude, request.Latitude);
            aggregate.UpdateLocation(newLocation, request.TimeOfUpdate);
            await _eventRepository.SaveAsync(aggregate);
            await _notificationService.Notify(new LocationUpdatedNotification(newLocation, request.TimeOfUpdate, request.MACAddress));
            return Unit.Value;
        }
    }
}
