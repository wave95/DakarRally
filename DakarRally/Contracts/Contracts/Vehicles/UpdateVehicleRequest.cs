namespace DakarRally.Contracts.Vehicles
{
    /// <summary>
    /// Update vehicle request.
    /// </summary>
    public class UpdateVehicleRequest : SaveVehicleRequest
    {
        /// <summary>
        /// Vehicle identifier.
        /// </summary>
        public int VehicleId { get; set; }
        
    }
}
