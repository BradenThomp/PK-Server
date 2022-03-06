using Application.Common.Notifications;
using Application.Common.Repository;
using Infrastructure.Notifications;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    /// <summary>
    /// Adds infrastructure services to the service collection so they can be accessed through dependency injection.
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// Adds infrastructure services to the service collection so they can be accessed through dependency injection.
        /// </summary>
        /// <param name="services">The service collection to add to.</param>
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddTransient<ISpeakerRepository, SpeakerRepository>();
            services.AddTransient<ITrackerRepository, TrackerRepository>();
            services.AddTransient<IRentalRepository, RentalRepository>();
            services.AddTransient<INotificationService, NotificationHub>();
        }
    }
}
