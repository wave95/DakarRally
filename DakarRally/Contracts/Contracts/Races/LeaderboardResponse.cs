using System.Collections.Generic;

namespace DakarRally.Contracts.Races
{
    /// <summary>
    /// Race leaderboard response.
    /// </summary
    public class LeaderboardResponse
    {
        /// <summary>
        /// Gets or sets the race identifier.
        /// </summary>
        public int RaceId { get; set; }

        /// <summary>
        /// Gets or sets the leaderboard.
        /// </summary>
        public List<VehicleLeaderboardStats> Leaderboard { get; set; }  
    }
}
