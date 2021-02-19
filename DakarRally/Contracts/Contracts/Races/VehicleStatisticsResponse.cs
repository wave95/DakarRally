using System;
using System.Collections.Generic;
using System.Text;

namespace DakarRally.Contracts.Races
{
    /// <summary>
    /// Vehicle statistics response.
    /// </summary>
    public class VehicleStatisticsResponse
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
        /// Vehicle distance.
        /// </summary>
        public string Distance { get; set; }

        /// <summary>
        /// Vehicle status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Start time in UTC format.
        /// </summary>
        public DateTime? StartTimeUtc { get; set; }

        /// <summary>
        /// Finish time in UTC format.
        /// </summary>
        public DateTime? FinishTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the light malfunctions count.
        /// </summary>
        public int LightMalfunctionsCount { get; set; }

        /// <summary>
        /// Vehicle heavy malfunctions count.
        /// </summary>
        public int HeavyMalfunctionsCount { get; set; }

        /// <summary>
        /// Number of hours Vehicle spent on repair.
        /// </summary>
        public decimal HoursRepairing { get; set; }
    }
}
