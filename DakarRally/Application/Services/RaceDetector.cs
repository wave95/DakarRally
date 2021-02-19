using DakarRally.Application.Interfaces;
using DakarRally.Domain.Entities;
using DakarRally.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DakarRally.Application.Services
{
    /// <summary>
    /// Race detector.
    /// </summary>
    public class RaceDetector : IRaceDetector
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RaceDetector"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public RaceDetector(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AnyRaceRunning()
        {
            return await _dbContext.Set<Race>().AnyAsync(x => x.Status == RaceStatus.Running);
        }

    }
}
