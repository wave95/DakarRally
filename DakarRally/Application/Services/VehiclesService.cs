using DakarRally.Application.Interfaces;
using DakarRally.Contracts;
using DakarRally.Contracts.Exceptions;
using DakarRally.Contracts.Vehicles;
using DakarRally.Domain.Entities;
using DakarRally.Domain.Enums;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.Application.Services
{
    public class  VehiclesService : DakarRallyService,  IVehiclesService
    {
        private IDbContext _dbContext;
        private IExceptionLogger _logger;

        public VehiclesService(IDbContext dbContext, IExceptionLogger logger)
        {
            _dbContext = dbContext;
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
        public async Task<Result> AddVehicleToRace(AddVehicleRequest request, CancellationToken cancellationToken = default)
        {
            List<string> errorsList = new List<string>();

            try
            {
                if (!ValidateVehicle(request, errorsList))
                {
                    return Result.Failure(errorsList);
                }

                var race = await _dbContext.GetBydIdAsync<Race>(request.RaceId);

                if (race == null)
                    throw new RaceNotFoundException();

                if (race.Status != RaceStatus.Pending)
                    throw new AddVehicleException(new RaceAlreadyStartedException());

                var vehicleSubtype = (VehicleSubtype)request.VehicleSubtype;

                var vehicleType = GetVehicleTypeBySubtype(vehicleSubtype);

                var vehicle = new Vehicle()
                {
                    RaceId = race.Id,
                    TeamName = request.TeamName,
                    ModelName = request.ModelName,
                    ManufacturingDate = request.ManufacturingDate,
                    VehicleType = vehicleType,
                    VehicleSubtype = vehicleSubtype,
                    Status = VehicleStatus.Pending,
                    Distance = decimal.Zero,
                    CreatedOnUtc = DateTime.UtcNow,
                };

                _dbContext.Insert(vehicle);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;

            }
            catch (Exception e)
            {
                return HandleException(e);
            }

        }

        public async Task<Result> RemoveVehicle(int vehicleId, CancellationToken cancellationToken = default)
        {
            List<string> errorsList = new List<string>();

            try
            {
                var vehicle = await _dbContext.GetBydIdAsync<Vehicle>(vehicleId);

                if (vehicle == null)
                {
                    throw new VehicleNotFoundException();
                }

                var race = await _dbContext.GetBydIdAsync<Race>(vehicle.RaceId);

                if (race.Status != RaceStatus.Pending)
                {
                    throw new AddVehicleException(new RaceAlreadyStartedException());
                }

                _dbContext.Remove(vehicle);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

        }
        
        public async Task<Result> UpdateVehicle(UpdateVehicleRequest request, CancellationToken cancellationToken = default)
        {
            List<string> errorsList = new List<string>();

            try
            {
                var vehicle = await _dbContext.GetBydIdAsync<Vehicle>(request.VehicleId);

                if (vehicle == null)
                {
                    throw new VehicleNotFoundException();
                }

                var race = await _dbContext.GetBydIdAsync<Race>(vehicle.RaceId);

                if (race.Status != RaceStatus.Pending)
                {
                    throw new UpdateVehicleException(new RaceAlreadyStartedException());
                }

                if (!ValidateVehicle(request, errorsList))
                {
                    return Result.Failure(errorsList);
                }

                UpdateVehicleProperties(vehicle, request);

                _dbContext.Update(vehicle);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;

            }
            catch (Exception e)
            {
                return HandleException(e);                
            }


        }

        public async Task<Result<VehicleResponse>> GetVehicleById(int vehicleId, CancellationToken cancellation = default)
        {
            List<string> errorsList = new List<string>();

            try
            {
                var vehicle = await _dbContext
                .Set<Vehicle>()
                .Include(x => x.Speed)
                .FirstOrDefaultAsync();

                if (vehicle == null)
                    throw new VehicleNotFoundException();

                var response = new VehicleResponse
                {
                    VehicleId = vehicle.Id,
                    RaceId = vehicle.RaceId,
                    TeamName = vehicle.TeamName,
                    ModelName = vehicle.ModelName,
                    ManufacturingDate = vehicle.ManufacturingDate,
                    Distance = $"{vehicle.Distance} km",
                    Speed = $"{vehicle.Speed.SpeedInKilometersPerHour} km/h",
                    Status = vehicle.Status.ToString(),
                    VehicleType = vehicle.VehicleType.ToString(),
                    VehicleSubtype = vehicle.VehicleSubtype.ToString()
                };

                return Result<VehicleResponse>.Success(response);
            }
            catch (Exception e)
            {
                return HandleException<VehicleResponse>(e);
            }
        }

        public async Task<Result<List<VehicleResponse>>> FindVehicles(FindVehiclesRequest request, CancellationToken cancellation = default)
        {
            List<string> errorsList = new List<string>();
            try
            {
                if (request.RaceId <= 0)
                    throw new ArgumentOutOfRangeException(nameof(request.RaceId), request.RaceId, Domain.Constants.Races.RaceIdMustBePositive);

                if (!ValidateFindVehiclesRequest(request, errorsList))
                    return Result<List<VehicleResponse>>.Failure(errorsList);

                var vehiclesList = await _dbContext.Set<Vehicle>()
                .Include(x => x.Speed)
                .AsNoTracking()
                .Where(vehicle =>
                    vehicle.RaceId == request.RaceId &&
                    (string.IsNullOrEmpty(request.Team) || vehicle.TeamName.ToLower().Contains(request.Team)) &&
                    (string.IsNullOrEmpty(request.Model) || vehicle.ModelName.ToLower().Contains(request.Model)) &&
                    (!request.ManufacturingDateFrom.HasValue || vehicle.ManufacturingDate >= request.ManufacturingDateFrom) &&
                    (!request.ManufacturingDateTo.HasValue || vehicle.ManufacturingDate <= request.ManufacturingDateTo) &&
                    (!request.DistanceFrom.HasValue || vehicle.Distance >= request.DistanceFrom) &&
                    (!request.DistanceTo.HasValue || vehicle.Distance <= request.DistanceTo))
                .OrderBy(request.SortOrder)
                .Select(vehicle => new VehicleResponse
                {
                    VehicleId = vehicle.Id,
                    RaceId = vehicle.RaceId,
                    TeamName = vehicle.TeamName,
                    ModelName = vehicle.ModelName,
                    ManufacturingDate = vehicle.ManufacturingDate,
                    Distance = $"{vehicle.Distance} km",
                    Speed = $"{vehicle.Speed.SpeedInKilometersPerHour} km/h",
                    Status = vehicle.Status.ToString(),
                    VehicleType = vehicle.VehicleType.ToString(),
                    VehicleSubtype = vehicle.VehicleSubtype.ToString()
                })
                .ToListAsync();

                return Result<List<VehicleResponse>>.Success(vehiclesList);

            }
            catch (Exception e)
            {
                return HandleException<List<VehicleResponse>>(e);
            }
        }

        #endregion

        #region Validation Methods

        /// <summary>
        /// Validates the <see cref="SaveVehicleRequest"> object.
        /// </summary>
        /// <param name="request">The vehicle creation request.</param>
        /// <param name="errorsList">Reference to an errors list.</param>
        private bool ValidateVehicle(SaveVehicleRequest request, List<string> errorsList)
        {
            if (string.IsNullOrWhiteSpace(request.TeamName))
                errorsList.Add(Domain.Constants.Vehicles.TeamNameRequired);

            if (request.TeamName.Length > Domain.Constants.Vehicles.TeamNameMaximumLength)
                errorsList.Add(Domain.Constants.Vehicles.TeamNameTooLong);

            if (string.IsNullOrWhiteSpace(request.ModelName))
                errorsList.Add(Domain.Constants.Vehicles.ModelNameRequired);

            if (request.ModelName.Length > Domain.Constants.Vehicles.ModelNameMaximumLength)
                errorsList.Add(Domain.Constants.Vehicles.ModelNameTooLong);

            if (!Enum.IsDefined(typeof(VehicleSubtype), request.VehicleSubtype))
            {
                errorsList.Add(Domain.Constants.Races.InvalidVelicleSubtype);
            }

            return errorsList.Count == 0; 
        }

        /// <summary>
        /// Validates the <see cref="FindVehiclesRequest"> object.
        /// </summary>
        /// <param name="request">The find vehicles request.</param>
        /// <param name="errorsList">Reference to an errors list.</param>
        private bool ValidateFindVehiclesRequest(FindVehiclesRequest request, List<string> errorsList)
        {
            if (typeof(VehicleResponse).GetProperty(request.SortOrder) != null)
                errorsList.Add(Domain.Constants.Vehicles.InvalidSortingCriteria);

            if (string.IsNullOrEmpty(request.Team)
                && string.IsNullOrEmpty(request.Model)
                && !request.ManufacturingDateFrom.HasValue
                && !request.ManufacturingDateTo.HasValue
                && !request.DistanceFrom.HasValue
                && !request.DistanceTo.HasValue)
                errorsList.Add(Domain.Constants.Vehicles.AtLeastOneFilter);

            return errorsList.Count == 0;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Returns the vehicle type based on the specified vehicle subtype.
        /// </summary>
        /// <param name="vehicleSubtype">The vehicle subtype.</param>
        private VehicleType GetVehicleTypeBySubtype(VehicleSubtype vehicleSubtype)
        {
            switch (vehicleSubtype)
            {
                case VehicleSubtype.Truck:
                    return VehicleType.Truck;
                case VehicleSubtype.TerrainCar:
                case VehicleSubtype.SportsCar:
                    return VehicleType.Car;
                case VehicleSubtype.CrossMotorcycle:
                case VehicleSubtype.SportMotorcycle:
                    return VehicleType.Motorcycle;
                default:
                    throw new ArgumentOutOfRangeException(nameof(vehicleSubtype), vehicleSubtype, null);
            }
        }

        /// <summary>
        /// Updates the vehicle properties based on update request.
        /// </summary>
        /// <param name="vehicle">The vehicle for update.</param>
        /// <param name="updateRequest">The request with updated property values.</param>
        private void UpdateVehicleProperties(Vehicle vehicle, UpdateVehicleRequest updateRequest)
        {
            vehicle.ModelName = updateRequest.ModelName;
            vehicle.TeamName = updateRequest.TeamName;
            vehicle.VehicleSubtype = (VehicleSubtype)updateRequest.VehicleSubtype;
            vehicle.ManufacturingDate = updateRequest.ManufacturingDate;

            vehicle.LastModifiedOnUtc = DateTime.UtcNow;
        }


        #endregion

    }
}
