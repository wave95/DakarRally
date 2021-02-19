using DakarRally.Domain.Enums;

namespace DakarRally.Domain.Entities
{
    /// <summary>
    /// Vehicle malfunction probabilities for a specific vehicle subtype.
    /// </summary>
    public class VehicleSubtypeMalfunctionProbability : Entity
    {
       
       /// <summary>
        /// Initializes a new instance of the <see cref="VehicleSubtypeMalfunctionProbability"/> class.
        /// </summary>
        public VehicleSubtypeMalfunctionProbability()
        {
        }

        /// <summary>
        /// Vehicle subtype.
        /// </summary>
        public VehicleSubtype VehicleSubtype => (VehicleSubtype)Id;

        /// <summary>
        /// Vehicle light malfunction probability.
        /// </summary>
        public decimal LightMalfunctionProbability { get; set; }

        /// <summary>
        /// Vehicle heavy malfunction probability.
        /// </summary>
        public decimal HeavyMalfunctionProbability { get; set; }
    }
}
