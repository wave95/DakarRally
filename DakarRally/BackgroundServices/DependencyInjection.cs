using DakarRally.BackgroundServices.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace DakarRally.BackgroundServices
{
    /// <summary>
    /// Contains the extensions method for registering dependencies.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {

            services.AddSingleton(typeof(IJob), typeof(DakarRallySimulator));

            return services;
        }
    }
}
