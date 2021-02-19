using System;

namespace DakarRally.Contracts.Vehicles
{
    /// <summary>
    /// Vehicle creation request.
    /// </summary>
    public class AddVehicleRequest : SaveVehicleRequest
    {
        /// <summary>
        /// Race identifier.
        /// </summary>
        public int RaceId { get; set; }

       
    }
}
