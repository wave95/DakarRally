using System;

namespace DakarRally.Contracts.Vehicles
{
    /// <summary>
    /// Find vehicle request
    /// </summary>
    public class FindVehiclesRequest
    {
        /// <summary>
        /// Race identifier.
        /// </summary>
        public int RaceId { get; set; }

        /// <summary>
        /// Vehicle team.
        /// </summary>
        public string Team { get; set; }

        /// <summary>
        /// Vehicle model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Vehicle manufacturing date from.
        /// </summary>
        public DateTime? ManufacturingDateFrom { get; set; }

        /// <summary>
        /// Veicle manufacturing date to.
        /// </summary>
        public DateTime? ManufacturingDateTo { get; set; }

        /// <summary>
        /// Vehicle status.
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Vehicle distance from.
        /// </summary>
        public decimal? DistanceFrom { get; }

        /// <summary>
        /// Vehicle distance to.
        /// </summary>
        public decimal? DistanceTo { get; }

        /// <summary>
        /// Order by.
        /// </summary>
        public string SortOrder { get; set; }
    }
}
