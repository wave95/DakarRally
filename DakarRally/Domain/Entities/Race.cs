using DakarRally.Domain.Enums;
using System;
using System.Collections.Generic;

namespace DakarRally.Domain.Entities
{
    public class Race : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Race"/> class.
        /// </summary>
        public Race()
        {
        }

        /// <summary>
        /// Race year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Length in kilometers.
        /// </summary>
        public decimal Length { get; set; }

        /// <summary>
        /// Race status.
        /// </summary>
        public RaceStatus Status { get; set; }

        /// <summary>
        /// Start time in UTC format.
        /// </summary>
        public DateTime? StartTimeUtc { get; set; }

        /// <summary>
        /// Number of hours that have passed from race start.
        /// </summary>
        public int? HoursFromRaceStart { get; set; }

        /// <summary>
        /// Finish time in UTC format.
        /// </summary>
        public DateTime? EndTimeUtc { get; set; }

        public List<Vehicle> Vehicles { get; set; }
    }
}
