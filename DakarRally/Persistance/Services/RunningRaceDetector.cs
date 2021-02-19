using System.Threading.Tasks;
using DakarRally.Application.Interfaces;
using DakarRally.Domain.Entities;
using DakarRally.Domain.Enums;
using DakarRally.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DakarRally.Persistence.Services
{
    /// <summary>
    /// Represents the running race checker.
    /// </summary>
    internal class RunningRaceDetector : IRunningRaceDetector
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RunningRaceDetector"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public RunningRaceDetector(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<bool> IsAnyRaceRunning()
        {
            return await _dbContext.Set<Race>().AnyAsync(x => x.Status == RaceStatus.Running);
        }
    }
}
