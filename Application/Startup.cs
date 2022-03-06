using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class Startup
    {
        /// <summary>
        /// Adds all services needed by the application layer to the service collection.
        /// These services are used for dependency injection.
        /// </summary>
        /// <param name="services">The collection of services.</param>
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
