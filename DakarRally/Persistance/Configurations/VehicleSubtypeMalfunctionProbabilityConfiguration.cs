using DakarRally.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DakarRally.Persistence.Configurations
{
    /// <summary>
    /// Contains the <see cref="VehicleSubtypeMalfunctionProbability"/> entity configuration.
    /// </summary>
    internal sealed class VehicleSubtypeMalfunctionProbabilityConfiguration : IEntityTypeConfiguration<VehicleSubtypeMalfunctionProbability>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<VehicleSubtypeMalfunctionProbability> builder)
        {
            builder.HasKey(vehicleSubtypeMalfunctionProbability => vehicleSubtypeMalfunctionProbability.Id);

            builder.Property(vehicleSubtypeMalfunctionProbability => vehicleSubtypeMalfunctionProbability.LightMalfunctionProbability).IsRequired();

            builder.Property(vehicleSubtypeMalfunctionProbability => vehicleSubtypeMalfunctionProbability.HeavyMalfunctionProbability).IsRequired();

            builder.Property(vehicleSubtypeMalfunctionProbability => vehicleSubtypeMalfunctionProbability.CreatedOnUtc).IsRequired();

            builder.Property(vehicleSubtypeMalfunctionProbability => vehicleSubtypeMalfunctionProbability.LastModifiedOnUtc).IsRequired(false);

            builder.Ignore(vehicleSubtypeMalfunctionProbability => vehicleSubtypeMalfunctionProbability.VehicleSubtype);
        }
    }
}
