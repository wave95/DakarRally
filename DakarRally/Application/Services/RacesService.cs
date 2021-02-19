using DakarRally.Application.Interfaces;
using DakarRally.Contracts;
using DakarRally.Contracts.Exceptions;
using DakarRally.Contracts.Races;
using DakarRally.Domain.Entities;
using DakarRally.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.Application.Services
{
    public class RacesService : DakarRallyService, IRacecService
    {
        private const decimal _defaultRaceLength = Domain.Constants.Races.DefaultRaceLenght;
        private static readonly VehicleStatus[] NotInRaceStatusese = {VehicleStatus.Broken, VehicleStatus.CompletedRace };

        private readonly IDbContext _dbContext;
        private readonly IRaceDetector _raceDetector; 
        private readonly IExceptionLogger _logger;

        public RacesService(IDbContext dbContext, IRaceDetector raceDetector, IExceptionLogger logger)
        {
            _dbContext = dbContext;
            _raceDetector = raceDetector;
            _logger = logger;
        }

        #region DakarRallyService Overrides
        protected override Result HandleException(Exception exception)
        {
            _logger.LoggException(exception);
            var errorsList = new List<string>();
            errorsList.Add(exception.Message);
            return Result.Failure(errorsList);           
        }
        protected override Result<ValueT> HandleException<ValueT>(Exception exception)
        {
            _logger.LoggException(exception);
            var errorsList = new List<string>();
            errorsList.Add(exception.Message);
            return Result<ValueT>.Failure(errorsList);
        }
        #endregion

        #region Business Methods
        public async Task<Result> CreateRace(CreateRaceRequest request, CancellationToken cancellationToken = default)
        {
            List<string> errorsList = new List<string>();

            try
            {
                if (!ValidateCreateRace(request, errorsList))
                {
                    return Result.Failure(errorsList);
                }

                var race = new Race()
                {
                    Year = request.Year,
                    Length = _defaultRaceLength
                };

                _dbContext.Insert(race);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;

            }
            catch (Exception e)
            {
                errorsList.Add(e.Message);
                return Result.Failure(errorsList);
            }

        }

        public async Task<Result> StartRace(int raceId, CancellationToken cancellationToken = default)
        {
            List<string> errorsList = new List<string>();

            try
            {
                if (await _raceDetector.AnyRaceRunning())
                {
                    throw new ThereIsARunningRaceException();
                }

                if (raceId <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(raceId), raceId, Domain.Constants.Races.RaceIdMustBePositive);
                }

                var race = await _dbContext
                    .Set<Race>()
                    .Include(x => x.Vehicles)
                    .Where(x => x.Id == raceId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (race == null)
                {
                    throw new RaceNotFoundException();
                }

                if (race.Status != (int)RaceStatus.Pending)
                {
                    throw new RaceAlreadyStartedException();
                }

                StartRace(race);

                _dbContext.Update<Race>(race);
                await _dbContext.SaveChangesAsync(cancellationToken);
                
                return Result.Success;      
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

        }

        public async Task<Result<LeaderboardResponse>> GetLeaderboard(int raceId, CancellationToken cancellationToken = default)
        {
            List<string> errorsList = new List<string>();

            try
            {
                if (raceId <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(raceId), raceId, Domain.Constants.Races.RaceIdMustBePositive);
                }

                var leaderboardVehicles = await _dbContext
                    .Set<Vehicle>()
                    .AsNoTracking()
                    .Where(x => x.RaceId == raceId)
                    .OrderBy(x => x.FinishTimeUtc.HasValue)
                    .ThenBy(x => x.FinishTimeUtc)
                    .ThenByDescending(x => x.Distance)
                    .Select(vehicle => new VehicleLeaderboardStats
                    {
                        VehicleId = vehicle.Id,
                        Distance = $"{vehicle.Distance} km",
                        FinishTime = vehicle.FinishTimeUtc,
                        VehicleSubtype = vehicle.VehicleSubtype.ToString()
                    })
                    .ToListAsync(cancellationToken);

                var response = new LeaderboardResponse
                {
                    RaceId = raceId,
                    Leaderboard = leaderboardVehicles
                    .Select((x, index) => new VehicleLeaderboardStats
                    {
                        Position = index + 1,
                        VehicleId = x.VehicleId,
                        Distance = x.Distance,
                        FinishTime = x.FinishTime,
                        VehicleSubtype = x.VehicleSubtype
                    }).ToList()
                };

                return Result<LeaderboardResponse>.Success(response);
            }
            catch (Exception e)
            {
                return HandleException<LeaderboardResponse>(e);
            }
        }

        public async Task<Result<LeaderboardTypeResponse>> GetLeaderboardForVehicleType(int raceId, int vehicleType, CancellationToken cancellationToken = default) 
        {
            List<string> errorsList = new List<string>();

            try
            {
                if (raceId <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(raceId), raceId, Domain.Constants.Races.RaceIdMustBePositive);
                }

                if (!Enum.IsDefined(typeof(VehicleType), vehicleType))
                {
                    errorsList.Add(Domain.Constants.Races.InvalidVehicleType);
                }

                if (errorsList.Count != 0)
                    return Result<LeaderboardTypeResponse>.Failure(errorsList);

                List<VehicleLeaderboardStats> leaderboardVehicles =
                await _dbContext
                    .Set<Vehicle>()
                    .AsNoTracking()
                    .Where(x => x.RaceId == raceId && x.VehicleType == (VehicleType)vehicleType)
                    .OrderBy(x => x.FinishTimeUtc.HasValue)
                    .ThenBy(x => x.FinishTimeUtc)
                    .ThenByDescending(x => x.Distance)
                    .Take(10)
                    .Select(vehicle => new VehicleLeaderboardStats
                    {
                        VehicleId = vehicle.Id,
                        Distance = $"{vehicle.Distance} km",
                        FinishTime = vehicle.FinishTimeUtc,
                        VehicleSubtype = vehicle.VehicleSubtype.ToString()
                    })
                    .ToListAsync(cancellationToken);

                var response = new LeaderboardTypeResponse
                {
                    RaceId = raceId,
                    VehicleType = vehicleType.ToString(),
                    Leaderboard = leaderboardVehicles.Select((x, index) => new VehicleLeaderboardStats
                    {
                        Position = index + 1,
                        VehicleId = x.VehicleId,
                        Distance = x.Distance,
                        FinishTime = x.FinishTime,
                        VehicleSubtype = x.VehicleSubtype
                    }).ToList()
                };

                return Result<LeaderboardTypeResponse>.Success(response);

            }
            catch(Exception e)
            {
                return HandleException<LeaderboardTypeResponse>(e);
            }

        }

        public async Task<Result<VehicleStatisticsResponse>> GetVehicleStatistic(int vehicleId, CancellationToken cancellationToken = default)
        {
            List<string> errorsList = new List<string>();

            try
            {
                if (vehicleId <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(vehicleId), vehicleId, Domain.Constants.Vehicles.VehicleIdMustBePositive);
                }

                var vehicle = await _dbContext.Set<Vehicle>()
                    .Include(x => x.RepairmentLength)
                    .Include(x => x.Malfunctions)
                    .Where(x => x.Id == vehicleId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (vehicle == null)
                    throw new VehicleNotFoundException();

                var lightMalfunctionsCount = vehicle.Malfunctions.Count(x => x.Type == MalfunctionType.Light);

                var response = new VehicleStatisticsResponse
                {
                    VehicleId = vehicle.Id,
                    RaceId = vehicle.RaceId,
                    Distance = $"{vehicle.Distance} km",
                    StartTimeUtc = vehicle.StartTimeUtc,
                    FinishTimeUtc = vehicle.FinishTimeUtc,
                    Status = vehicle.Status.ToString(),
                    LightMalfunctionsCount = lightMalfunctionsCount,
                    HeavyMalfunctionsCount = vehicle.Malfunctions.Count(x => x.Type == MalfunctionType.Heavy),
                    HoursRepairing = lightMalfunctionsCount * vehicle.RepairmentLength
                };

                return Result<VehicleStatisticsResponse>.Success(response);

            }
            catch(Exception e)
            {
                return HandleException<VehicleStatisticsResponse>(e);
            }

        }

        public async Task<Result<RaceStatusResponse>> GetRaceStatus(int raceId, CancellationToken cancellationToken = default)
        {
            try
            {

                if (raceId <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(raceId), raceId, Domain.Constants.Races.RaceIdMustBePositive);
                }

                var race = await _dbContext
                    .Set<Race>()
                    .AsNoTracking()
                    .Where(x => x.Id == raceId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (race == null)
                {
                    throw new RaceNotFoundException();
                }

                var vehicleStatistics = await (
                    from vehicle in _dbContext.Set<Vehicle>().AsNoTracking()
                    where vehicle.RaceId == race.Id
                    group vehicle by new { vehicle.VehicleType, vehicle.Status }
                    into grouping
                    select new
                    {
                        Type = grouping.Key.VehicleType.ToString(),
                        Status = grouping.Key.Status.ToString(),
                        Count = grouping.Count()
                    }).ToListAsync(cancellationToken);

                var response = new RaceStatusResponse
                {
                    RaceId = race.Id,
                    StartTimeUtc = race.StartTimeUtc,
                    FinishTimeUtc = race.EndTimeUtc,
                    Status = race.Status.ToString(),
                    VehiclesByTypeList = vehicleStatistics
                    .GroupBy(x => x.Type)
                    .Select(x => new VehicleGroup
                    {
                        GroupedBy = x.Key,
                        Count = x.Sum(g => g.Count)
                    }).ToList(),
                    VehiclesByStatusList = vehicleStatistics.GroupBy(x => x.Status)
                    .Select(x => new VehicleGroup
                    {
                        GroupedBy = x.Key,
                        Count = x.Sum(g => g.Count)
                    }).ToList(),
                };

                return Result<RaceStatusResponse>.Success(response);
            }
            catch (Exception e)
            {
               return HandleException<RaceStatusResponse>(e);
            }

        }

        public async Task<Result> SimulateRaceHour(Race race, List<Vehicle> vehicles, CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (Vehicle vehicle in vehicles.Where(x => !NotInRaceStatusese.Contains(x.Status)))
                {
                    IncrementVehicleHours(vehicle);

                    if (vehicle.Status == VehicleStatus.WaitingForRepair && !TryRepairVehicle(vehicle))
                    {
                        continue;
                    }

                    decimal malfunctionProbability = (decimal)new Random().NextDouble();

                    if (vehicle.MalfunctionProbability.LightMalfunctionProbability >= malfunctionProbability)
                    {
                        TryAddMalfunction(vehicle, MalfunctionType.Light);
                        continue;
                    }

                    if (vehicle.MalfunctionProbability.HeavyMalfunctionProbability >= malfunctionProbability)
                    {
                        TryAddMalfunction(vehicle, MalfunctionType.Heavy);

                        continue;
                    }

                    AddVehicleHourlyDistance(vehicle);

                    if (vehicle.Distance == _defaultRaceLength)
                    {
                        VehicleCompleteRace(vehicle);
                    }
                }

                return Result.Success;

            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        public async Task<Result> CompleteRace(Race race, CancellationToken cancellationToken = default)
        {
            try
            {
                CompleteRace(race);

                return Result.Success;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }            
        }

        private void StartRace(Race race)
        {
            race.Status = RaceStatus.Running;
            race.StartTimeUtc = DateTime.UtcNow;

            foreach (var vehicle in race.Vehicles)
            {
                VehicleStartRace(vehicle);
            }
        }

        private void CompleteRace(Race race)
        {
            race.Status = RaceStatus.Finished;
            race.EndTimeUtc = DateTime.UtcNow;

            foreach (var vehicle in race.Vehicles)
            {
                VehicleCompleteRace(vehicle);
            }
        }

        #endregion

        #region Validation Methods
        /// <summary>
        /// Validates the <see cref="CreateRaceRequest"> object.
        /// </summary>
        /// <param name="request">The race creation request.</param>
        /// <param name="errorsList">Reference to an errors list.</param>
        private bool ValidateCreateRace(CreateRaceRequest request, List<string> errorsList)
        {
            if (request.Year < DateTime.Now.Year)
                errorsList.Add(Domain.Constants.Races.YearInThePast);

            return errorsList.Count == 0;
        }
        #endregion region

        #region Helpers

        private void IncrementVehicleHours(Vehicle vehicle)
        {
            vehicle.HoursFromRaceStart = (vehicle.HoursFromRaceStart ?? 0) + 1;
        }

        private void AddVehicleHourlyDistance(Vehicle vehicle)
        {
            var distance = vehicle.Distance + vehicle.Speed.SpeedInKilometersPerHour * 1.0m;

            vehicle.Distance = (distance > _defaultRaceLength) ? _defaultRaceLength : distance; 
        }

        public void IncrementRaceHours(Race race)
        {
            race.HoursFromRaceStart = (race.HoursFromRaceStart ?? 0) + 1;
        }

        private bool TryRepairVehicle(Vehicle vehicle)
        {
            vehicle.HoursUntilRepaired -= 1;

            if (vehicle.HoursUntilRepaired > 0)
            {
                return false;
            }

            vehicle.Status = VehicleStatus.Racing;

            vehicle.HoursUntilRepaired = null;

            return true;

        }

        private bool TryAddMalfunction(Vehicle vehicle, MalfunctionType malfunctionType)
        {
            if (vehicle.Status == VehicleStatus.Pending || vehicle.Status == VehicleStatus.WaitingForRepair || vehicle.Status == VehicleStatus.Broken)
                return false;

            vehicle.Malfunctions
                .Add(new Malfunction()
                {
                    VehicleId = vehicle.Id,
                    Type = malfunctionType,
                    CreatedOnUtc = DateTime.Now
                });

            vehicle.Status = malfunctionType == MalfunctionType.Light ? VehicleStatus.WaitingForRepair : VehicleStatus.Broken;
            
            return true;

        }

        private void VehicleStartRace(Vehicle vehicle)
        {
            vehicle.Status = VehicleStatus.Racing;
            vehicle.StartTimeUtc = DateTime.UtcNow;
        }

        private void VehicleCompleteRace(Vehicle vehicle)
        {
            vehicle.Status = VehicleStatus.CompletedRace;

            vehicle.FinishTimeUtc = DateTime.UtcNow;
        }
        #endregion


    }
}
