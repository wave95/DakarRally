namespace DakarRally.Contracts.Races
{
    /// <summary>
    /// Represents the group of vehicles.
    /// </summary>
    public class VehicleGroup
    {
        /// <summary>
        /// Type or.
        /// </summary>
        public string GroupedBy { get; set; }

        /// <summary>
        /// Gets or sets the number of vehicles.
        /// </summary>
        public int Count { get; set; }
    }
}
