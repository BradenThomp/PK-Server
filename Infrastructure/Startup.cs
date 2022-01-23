using Application.Common.Notifications;
using Application.Common.Repository;
using Infrastructure.Notifications;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Startup
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddTransient<ISpeakerRepository, SpeakerRepository>();
            services.AddTransient<ITrackerRepository, TrackerRepository>();
            services.AddTransient<IRentalRepository, RentalRepository>();
            services.AddTransient<INotificationService, NotificationHub>();
        }
    }
}
