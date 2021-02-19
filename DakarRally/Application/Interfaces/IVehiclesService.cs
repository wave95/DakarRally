using DakarRally.Contracts;
using DakarRally.Contracts.Vehicles;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.Application.Interfaces
{
    public interface IVehiclesService
    {
        public Task<Result> AddVehicleToRace(AddVehicleRequest request, CancellationToken cancellationToken = default);

        public Task<Result> RemoveVehicle(int vehicleId, CancellationToken cancellationToken = default);

        public Task<Result> UpdateVehicle(UpdateVehicleRequest request, CancellationToken cancellationToken = default);

        public Task<Result<VehicleResponse>> GetVehicleById(int vehicleId, CancellationToken cancellation = default);

        Task<Result<List<VehicleResponse>>> FindVehicles(FindVehiclesRequest request, CancellationToken cancellation = default);
    }
}
