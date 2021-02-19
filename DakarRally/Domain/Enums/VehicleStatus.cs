namespace DakarRally.Domain.Enums
{
    /// <summary>
    /// Vehicle status enumeration.
    /// </summary>
    public enum VehicleStatus
    {
        /// <summary>
        /// The vehicle is pending for race to begin.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// The vehicle is in the running race.
        /// </summary>
        Racing = 2,

        /// <summary>
        /// The vehicle has a light malfunction and it is waiting for repairment.
        /// </summary>
        WaitingForRepair = 3,

        /// <summary>
        /// The vehicle has suffered a heavy malfunction and it is broken.
        /// </summary>
        Broken = 4,

        /// <summary>
        /// The vehicle has successfully completed the race.
        /// </summary>
        CompletedRace = 5
    }
}
