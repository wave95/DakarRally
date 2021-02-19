namespace DakarRally.Domain.Enums
{
    /// <summary>
    /// Race status enumeration.
    /// </summary>
    public enum RaceStatus
    {
        /// <summary>
        /// The pending status. The race has not yet started.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// The race is running.
        /// </summary>
        Running = 1,

        /// <summary>
        /// The race is completed, all of the vehicles have finished the race or have been eliminated.
        /// </summary>
        Finished = 2,
    }
}
