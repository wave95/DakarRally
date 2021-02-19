using System;

namespace DakarRally.Contracts.Races
{
    /// <summary>
    /// Vehicle leaderboeard stats.
    /// </summary>
    public class VehicleLeaderboardStats
    {
        /// <summary>
        /// Vehicle position.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Vehicle identifier.
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Vehicle distance.
        /// </summary>
        public string Distance { get; set; }

        /// <summary>
        /// vehicle finish time.
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// Vehicle subtype string value.
        /// </summary>
        public string VehicleSubtype { get; set; }
    }
}
