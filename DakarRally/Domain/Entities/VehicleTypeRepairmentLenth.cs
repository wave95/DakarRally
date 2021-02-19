using DakarRally.Domain.Enums;

namespace DakarRally.Domain.Entities
{
    /// <summary>
    /// RVehicle Type Repairment Length.
    /// </summary>
    public class VehicleTypeRepairmentLength : Entity
    {    
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTypeRepairmentLength"/> class.
        /// </summary>
        public VehicleTypeRepairmentLength()
        {
        }

        /// <summary>
        /// Vehicle type.
        /// </summary>
        public VehicleType VehicleType
        {
            get { return (VehicleType)Id; }
        }
        
        /// <summary>
        /// Repairment length in hours.
        /// </summary>
        public int RepairmentLengthInHours { get; set; }
    }
}
