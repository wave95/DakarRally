namespace DakarRally.Domain.Enums
{
    /// <summary>
    /// Malfunction types enumeration.
    /// </summary>
    public enum MalfunctionType
    {
        /// <summary>
        /// The light malfunction that can be repaired.
        /// </summary>
        Light = 1,

        /// <summary>
        /// The heavy malfunction that eliminates the vehicle.
        /// </summary>
        Heavy = 2
    }
}
