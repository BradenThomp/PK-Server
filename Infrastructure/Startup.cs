using Application.Common.Notifications;
using Application.Common.Repository;
using FluentScheduler;
using Infrastructure.Emails;
using Infrastructure.Notifications;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;

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

        public static void UseBackgroundScheduler(this IServiceProvider serviceProvider)
        {
            JobManager.Initialize(new EmailJobRegistry(serviceProvider.GetRequiredService<IRentalRepository>()));
        }

        public static void Shutdown()
        {
            JobManager.StopAndBlock();
        }
    }
}
