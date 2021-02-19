using System;

namespace DakarRally.Persistence.Interfaces
{
    /// <summary>
    /// Current date and time.
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        /// Current date and time in UTC format.
        /// </summary>
        DateTime UtcNow { get; }
    }
}