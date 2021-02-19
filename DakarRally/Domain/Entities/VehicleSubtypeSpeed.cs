using DakarRally.Domain.Enums;

/// <summary>
/// Vehicle subtyoe speed in km per hour.
/// </summary>
namespace DakarRally.Domain.Entities
{
    public class VehicleSubtypeSpeed : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleSubtypeSpeed"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        public VehicleSubtypeSpeed()
        {
        }

        /// <summary>
        /// Vehicle subtype.
        /// </summary>
        public VehicleSubtype VehicleSubtype => (VehicleSubtype)Id;

        /// <summary>
        /// Seed in kilometers per hour.
        /// </summary>
        public decimal SpeedInKilometersPerHour { get;  set; }
    }
}
