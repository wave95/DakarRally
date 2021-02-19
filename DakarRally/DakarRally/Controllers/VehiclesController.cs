using API.Constants;
using DakarRally.Api.Controllers;
using DakarRally.Application.Interfaces;
using DakarRally.Contracts;
using DakarRally.Contracts.Vehicles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{

    /// <summary>
    /// The vehicles controller.
    /// </summary>
    public class VehiclesController : DakarRallyController
    {
        private IVehiclesService _vehiclesService;


        public VehiclesController(IVehiclesService vehiclesService)
        {
            _vehiclesService = vehiclesService;
        }

        /// <summary>
        /// Creates the vehicle based on the specified request.
        /// </summary>
        /// <param name="request">The create vehicle request.</param>
        [HttpPost(Routes.Vehicles.AddVehicle)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DakarRallyApplicationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVehicle([FromBody] AddVehicleRequest request)
        {
            var result = await _vehiclesService.AddVehicleToRace(request);

            return HandleResult(result);

        }

        /// <summary>
        /// Updates the vehicle with the specified identifier.
        /// </summary>
        /// <param name="request">The update vehicle request.</param>
        [HttpPut(Routes.Vehicles.UpdateVehicle)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DakarRallyApplicationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVehicle([FromBody] UpdateVehicleRequest request)
        {
            var result = await _vehiclesService.UpdateVehicle(request);

            return HandleResult(result);
        }


        /// <summary>
        /// Removes the vehicle with the specified identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        [HttpDelete(Routes.Vehicles.RemoveVehicle)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(DakarRallyApplicationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveVehicle(int vehicleId)
        {
            var result = await _vehiclesService.RemoveVehicle(vehicleId);

            return HandleResult(result);
        }

        /// <summary>
        /// Gets the vehicle statistics for the vehicle with the specified identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        [HttpGet(Routes.Vehicles.GetVehicleById)]
        [ProducesResponseType(typeof(VehicleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehicleById(int vehicleId)
        {
            var result = await _vehiclesService.GetVehicleById(vehicleId);

            return HandleObjectResult(result);
        }

        /// <summary>
        /// Gets the vehicles for the specified parameters.
        /// </summary>
        [HttpGet(Routes.Vehicles.GetVehicles)]
        [ProducesResponseType(typeof(List<VehicleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DakarRallyApplicationError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindVehicles([FromBody] FindVehiclesRequest request)
        {
            var result = await _vehiclesService.FindVehicles(request);

            return HandleObjectResult(result);
        }
    }
}
