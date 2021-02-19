using System;
using System.Collections.Generic;

namespace DakarRally.Contracts.Races
{
    /// <summary>
    /// Race status response.
    /// </summary>
    public class RaceStatusResponse
    {
        /// <summary>
        /// Race identifier.
        /// </summary>
        public int RaceId { get; set; }

        /// <summary>
        /// Race status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Race start time in UTC format.
        /// </summary>
        public DateTime? StartTimeUtc { get; set; }

        /// <summary>
        /// Race finish time in UTC format.
        /// </summary>
        public DateTime? FinishTimeUtc { get; set; }

        /// <summary>
        /// Vehicles grouped by vehicle status.
        /// </summary>
        public List<VehicleGroup> VehiclesByStatusList { get; set; }

        /// <summary>
        /// Vvehicles grouped by vehicle type.
        /// </summary>
        public List<VehicleGroup> VehiclesByTypeList { get; set; }
    }
}
