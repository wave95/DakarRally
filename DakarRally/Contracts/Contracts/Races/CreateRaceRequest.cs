namespace DakarRally.Contracts.Races
{
    /// <summary>
    /// Request for race creation.
    /// </summary>
    public class CreateRaceRequest
    {
        /// <summary>
        /// The race year
        /// </summary>
        public int Year { get; set; }
    }
}