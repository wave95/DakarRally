using DakarRally.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DakarRally.Domain.Entities
{
    /// <summary>
    /// Represents the vehicle entity.
    /// </summary>
    public class Vehicle : Entity
    {
        private readonly List<Malfunction> _malfunctions = new List<Malfunction>();

        ///// <summary>
        ///// Initializes a new instance of the <see cref="Vehicle"/> class.
        ///// </summary>
        ///// <param name="race">The race.</param>
        ///// <param name="teamName">The team name.</param>
        ///// <param name="modelName">The model name.</param>
        ///// <param name="manufacturingDate">The manufacturing date.</param>
        ///// <param name="vehicleSubtype">The vehicle subtype.</param>
        //public Vehicle(int raceId, string teamName, string modelName, DateTime manufacturingDate, VehicleType vehicleType,VehicleSubtype vehicleSubtype)
        //{            
        //    RaceId = raceId;
        //    TeamName = teamName;
        //    ModelName = modelName;
        //    ManufacturingDate = manufacturingDate.Date;
        //    VehicleType = VehicleType;
        //    VehicleSubtype = vehicleSubtype;
        //    Status = VehicleStatus.Pending;
        //    Distance = decimal.Zero;
        //    CreatedOnUtc = DateTime.UtcNow;
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        public Vehicle()
        {
        }

        /// <summary>
        /// Gets the race identifier.
        /// </summary>
        public int RaceId { get;  set; }

        /// <summary>
        /// Gets the team name.
        /// </summary>
        public string TeamName { get;  set; }

        /// <summary>
        /// Gets the model name.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Gets the manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get;  set; }

        /// <summary>
        /// Gets the vehicle type.
        /// </summary>
        public VehicleType VehicleType
        {
            get { return (VehicleType)VehicleTypeId; }
            set { VehicleTypeId = (int)value; }
        }

        /// <summary>
        /// Gets the vehicle type identifier.
        /// </summary>
        public int VehicleTypeId { get; set; }

        /// <summary>
        /// Gets the vehicle subtype.
        /// </summary>
        public VehicleSubtype VehicleSubtype
        {
            get { return (VehicleSubtype)VehicleSubtypeId; }
            set { VehicleSubtypeId = (int)value; }
        }

        /// <summary>
        /// Vehicle subtype identifier.
        /// </summary>
        public int VehicleSubtypeId { get; set; }

        /// <summary>
        /// Vehicle status.
        /// </summary>
        public VehicleStatus Status { get; set; }

        /// <summary>
        /// Number of hours left until the vehicle is repaired.
        /// </summary>
        public int? HoursUntilRepaired { get; set; }

        /// <summary>
        /// Fistance in kilometers the vehicle has covered.
        /// </summary>
        public decimal Distance { get; set; }

        /// <summary>
        /// Race start time in UTC format.
        /// </summary>
        public DateTime? StartTimeUtc { get; set; }

        /// <summary>
        /// The number of hours that have passed from race start.
        /// </summary>
        public int? HoursFromRaceStart { get; set; }

        /// <summary>
        /// The race finish time in UTC format.
        /// </summary>
        public DateTime? FinishTimeUtc { get; set; }

        /// <summary>
        /// Vehicle epairment length.
        /// </summary>
        public decimal RepairmentLength { get; set; }

        /// <summary>
        /// Vehicle speed.
        /// </summary>
        public VehicleSubtypeSpeed Speed { get; set; }

        /// <summary>
        /// Vehicle malfunction probability.
        /// </summary>
        public VehicleSubtypeMalfunctionProbability MalfunctionProbability { get; set; }

        /// Vehicle malfunctions.
        /// </summary>
        public List<Malfunction> Malfunctions { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether or not the vehicle is in pending status.
        /// </summary>
        private bool Pending { get { return Status == VehicleStatus.Pending; } }

        /// <summary>
        /// Gets a value indicating whether or not the vehicle is waiting for repair.
        /// </summary>
        private bool WaitingForRepair { get { return Status == VehicleStatus.WaitingForRepair && HoursUntilRepaired.HasValue; } }

        /// <summary>
        /// Gets a value indicating whether or not the vehicle is broken.
        /// </summary>
        private bool Broken { get { return Status == VehicleStatus.Broken; } }
    }
}
