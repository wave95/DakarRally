using DakarRally.Application.Interfaces;
using DakarRally.Application.Services;
using DakarRally.Domain.Entities;
using DakarRally.Domain.Enums;
using DakarRally.Persistence;
using DakarRally.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.BackgroundServices.Tasks
{
    /// <summary>
    /// Dark Rally Simulator background service
    /// </summary>
    [DisallowConcurrentExecution]
    public class DakarRallySimulator : IJob
    {
        private readonly ILogger<DakarRallySimulator> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDateTime _dateTime;
        private readonly Random _random;


        /// <summary>
        /// Initializes a new instance of the <see cref="DakarRallySimulator"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="dateTime">The date and time.</param>
        public DakarRallySimulator(ILogger<DakarRallySimulator> logger, IServiceProvider serviceProvider, IDateTime dateTime)
        {            
            _logger = logger;
            _serviceProvider = serviceProvider;
            _dateTime = dateTime;
            _random = new Random();
        }

        /// <inheritdoc />
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Dakar Rally simulation started!");

            await Task.Delay(3000);

            while (!context.CancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Dakar Rally simulator background service is doing background work.");

                using IServiceScope scope = _serviceProvider.CreateScope();

                DakarRallyDbContext dbContext = scope.ServiceProvider.GetRequiredService<DakarRallyDbContext>();

                var race = await dbContext.Set<Race>()
                    .SingleOrDefaultAsync(x => x.Status == RaceStatus.Running, context.CancellationToken);

                if (race != null)
                {
                    await SimulateOneHour(race, dbContext);
                }

                await Task.Delay(2000);
            }
            _logger.LogInformation("Dakar Rally simulation finished!");

            await Task.CompletedTask;

        }

        /// <summary>
        /// Simulates one hour of race time.
        /// </summary>
        /// <param name="race">The race that is currently running.</param>
        /// <param name="dbContext">The database context.</param>
        private async Task SimulateOneHour(Race race, DakarRallyDbContext dbContext)
        {
            List<Vehicle> vehicles = await dbContext.Set<Vehicle>()
               .Include(x => x.RepairmentLength)
               .Include(x => x.Speed)
               .Include(x => x.MalfunctionProbability)
               .Include(x => x.Malfunctions)
               .Where(x => x.RaceId == race.Id)
               .ToListAsync();

            VehicleStatus[] completeStatuses = { VehicleStatus.Broken, VehicleStatus.CompletedRace };

            using IServiceScope scope = _serviceProvider.CreateScope();

            RacesService _raceService = scope.ServiceProvider.GetRequiredService<RacesService>();

            await _raceService.SimulateRaceHour(race, vehicles);

            if (vehicles.All(x => completeStatuses.Contains(x.Status)))
            {
                await _raceService.CompleteRace(race);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}

