using System;

namespace DakarRally.Contracts.Vehicles
{
    public abstract class SaveVehicleRequest
    {
        /// <summary>
        /// Vehicle team name.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Vehicle  model name.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Vehicle  manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; set; }

        /// <summary>
        /// Vehicle  subtype.
        /// </summary>
        public int VehicleSubtype { get; set; }
    }
}
