using DakarRally.Application.Interfaces;
using DakarRally.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DakarRally.Application
{
    /// <summary>
    /// Contains the extensions method for registering dependencies in the DI framework.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRacecService), typeof(RacesService));

            services.AddScoped(typeof(IVehiclesService), typeof(VehiclesService));

            services.AddScoped(typeof(IRaceDetector), typeof(RaceDetector));

            services.AddScoped(typeof(IExceptionLogger), typeof(ExceptionLogger));

            return services;
        }
    }
}
