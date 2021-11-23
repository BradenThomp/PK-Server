using Application.Common.Repository;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Startup
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddSingleton<IEventRepository, InMemoryEventRepository>();
        }
    }
}
