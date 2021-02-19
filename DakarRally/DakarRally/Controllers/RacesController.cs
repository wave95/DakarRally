using API;
using API.Constants;
using DakarRally.Application.Interfaces;
using DakarRally.Contracts.Races;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DakarRally.Api.Controllers
{
    /// <summary>
    /// Races controller.
    /// </summary>
    public class RacesController : DakarRallyController
    {
        private IRacecService _racecService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RacesController"/> class.
        /// </summary>
        /// <param name="racecService">Races service.</param>
        public RacesController(IRacecService racecService)
        {
            _racecService = racecService;
        }

        /// <summary>
        /// Creates the race based on the specified request.
        /// </summary>
        /// <param name="request">The create race request.</param>
        [HttpPost(Routes.Races.CreateRace)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DakarRallyApplicationError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRace([FromBody] CreateRaceRequest request)
        {
            var result =  await _racecService.CreateRace(request);

            return HandleResult(result);
        }

        /// <summary>
        /// Starts the race based on the specified request.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        [HttpPost(Routes.Races.StartRace)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DakarRallyApplicationError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> StartRace(int raceId)
        {
            var result = await _racecService.StartRace(raceId);

            return HandleResult(result);
        }

        /// <summary>
        /// Gets the race leaderboard for the race with the specified identifier.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        [HttpPost(Routes.Races.GetLeaderboard)]
        [ProducesResponseType(typeof(LeaderboardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DakarRallyApplicationError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLeaderboard(int raceId) 
        {
            var result = await _racecService.GetLeaderboard(raceId);

            return HandleObjectResult(result);
        }

        /// <summary>
        /// Gets the race leaderboard for the race with the specified identifier.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        /// <param name="vehicleType">The vehicle type.</param>
        [HttpGet(Routes.Races.GetLeaderboardForType)]
        [ProducesResponseType(typeof(LeaderboardTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DakarRallyApplicationError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRaceLeaderboardForVehicleType(int raceId, int vehicleType)
        {
            var result = await _racecService.GetLeaderboardForVehicleType(raceId, vehicleType);

            return HandleObjectResult<LeaderboardTypeResponse>(result);
        }

        /// <summary>
        /// Gets the vehicle statistics for the vehicle with the specified identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        [HttpGet(Routes.Vehicles.GetVehicleStatistics)]
        [ProducesResponseType(typeof(VehicleStatisticsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehicleStatistics(int vehicleId)
        {
            var result = await _racecService.GetVehicleStatistic(vehicleId);

            return HandleObjectResult<VehicleStatisticsResponse>(result);
        }

        /// <summary>
        /// Gets the race status for the race with the specified identifier.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        [HttpGet(Routes.Races.GetRaceStatus)]
        [ProducesResponseType(typeof(RaceStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRaceStatus(int raceId) 
        {
            var result = await _racecService.GetRaceStatus(raceId);

            return HandleObjectResult<RaceStatusResponse>(result);
        }
    }
}
