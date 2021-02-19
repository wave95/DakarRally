using System;
using DakarRally.Domain.Enums;

namespace DakarRally.Domain.Entities
{
    /// <summary>
    /// Represents the malfunction that can occur on a vehicle during the race.
    /// </summary>
    public class Malfunction : Entity
    {        
        /// <summary>
        /// Initializes a new instance of the <see cref="Malfunction"/> class.
        /// </summary>
        public Malfunction()
        {
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets the malfunction type.
        /// </summary>
        public MalfunctionType Type { get; set; }

    }
}
