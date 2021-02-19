using DakarRally.Persistence.Interfaces;
using System;

namespace DakarRally.Persistence
{
    /// <summary>
    /// Represents the current machine date and time.
    /// </summary>
    internal sealed class DakarRallyDateTime : IDateTime
    {
        /// <inheritdoc />
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
