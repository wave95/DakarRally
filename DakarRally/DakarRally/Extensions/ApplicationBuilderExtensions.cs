using DakarRally.Persistence;
using DakarRally.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Ensures that the in-memory database is created.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The same application builder.</returns>
        internal static IApplicationBuilder EnsureDatabaseCreated(this IApplicationBuilder builder)
        {
            using IServiceScope serviceScope = builder.ApplicationServices.CreateScope();

            using DakarRallyDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<DakarRallyDbContext>();

            dbContext.Database.EnsureCreated();

            dbContext.SeedDatabase();

            return builder;
        }
    }
}
