using Application.Common.Notifications;
using Application.Common.Repository;
using Domain.Aggregates;
using Infrastructure.Notifications;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Projections;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Startup
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<ISpeakerRepository, SpeakerRepository>();
            services.AddTransient<INotificationService, NotificationHub>();
            services.AddTransient<IProjectionWriter<Tracker>, TrackerProjectionRepository>();
            services.AddTransient<IProjectionWriter<Rental>, RentalProjectionRepository>();
            services.AddTransient<ITrackerProjectionRepository, TrackerProjectionRepository>();
        }
    }
}
