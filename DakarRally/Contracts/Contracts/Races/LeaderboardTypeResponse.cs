using System;
using System.Collections.Generic;
using System.Text;

namespace DakarRally.Contracts.Races
{
    /// <summary>
    /// Leaderboard for vehicle type.
    /// </summary>
    public class LeaderboardTypeResponse
    {
        /// <summary>
        /// Race identifier.
        /// </summary>
        public int RaceId { get; set; }

        /// <summary>
        /// Vehicle type.
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// Leaderboard stats list.
        /// </summary>
        public List<VehicleLeaderboardStats> Leaderboard { get; set; }
    }
}
