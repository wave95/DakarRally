using System;

namespace DakarRally.Contracts.Vehicles
{
    /// <summary>
    /// Represents Vehicle response.
    /// </summary>
    public class VehicleResponse
    {
        /// <summary>
        /// Vehicle identifier.
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Vehicle race identifier.
        /// </summary>
        public int RaceId { get; set; }

        /// <summary>
        /// Vehicle team name.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Vehicle model name.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Vehicle manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; set; }

        /// <summary>
        /// Vehicleehicle type.
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// Vehicle subtype.
        /// </summary>
        public string VehicleSubtype { get; set; }

        /// <summary>
        /// Vehicle status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Vehicle distance.
        /// </summary>
        public string Distance { get; set; }

        /// <summary>
        /// Vehicle speed.
        /// </summary>
        public string Speed { get; set; }
    }
}
