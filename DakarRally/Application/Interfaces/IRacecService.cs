using DakarRally.Contracts;
using DakarRally.Contracts.Races;
using DakarRally.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.Application.Interfaces
{
    public interface IRacecService
    {
        Task<Result> CreateRace(CreateRaceRequest request, CancellationToken cancellationToken = default);

        Task<Result> StartRace(int raceIdentifier, CancellationToken cancellationToken = default);

        Task<Result<LeaderboardResponse>> GetLeaderboard(int raceId, CancellationToken cancellationToken = default);

        Task<Result<LeaderboardTypeResponse>> GetLeaderboardForVehicleType(int raceId, int vehicleType, CancellationToken cancellationToken = default);

        Task<Result<VehicleStatisticsResponse>> GetVehicleStatistic(int vehicleId, CancellationToken cancellationToken = default);

        Task<Result<RaceStatusResponse>> GetRaceStatus(int raceId, CancellationToken cancellation = default);

        Task<Result> SimulateRaceHour(Race race, List<Vehicle> vehicles, CancellationToken cancellationToken = default);

        Task<Result> CompleteRace(Race race, CancellationToken cancellationToken = default);


    }
}
