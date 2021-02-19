using System.Data;
using DakarRally.Application.Interfaces;
using DakarRally.Persistence.Interfaces;
using DakarRally.Persistence.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DakarRally.Persistence
{
    /// <summary>
    /// Contains the extensions method for registering dependencies.
    /// </summary>
    public static class DependencyInjection
    {
        private static SqliteConnection _sqliteConnection;

        /// <summary>
        /// Registers with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            _sqliteConnection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection"));

            if (_sqliteConnection.State != ConnectionState.Open)
            {
                _sqliteConnection.Open();
            }

            services.AddDbContext<DakarRallyDbContext>(options => options.UseSqlite(_sqliteConnection));

            services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<DakarRallyDbContext>());

            services.AddScoped<IRunningRaceDetector, RunningRaceDetector>();

            services.AddTransient<IDateTime, DakarRallyDateTime>();

            services.AddSingleton<IDbConnection>(_ => _sqliteConnection);

            return services;
        }
    }
}
