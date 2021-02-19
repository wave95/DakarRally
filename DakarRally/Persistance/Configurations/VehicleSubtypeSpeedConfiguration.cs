using DakarRally.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DakarRally.Persistence.Configurations
{
    /// <summary>
    /// Contains the <see cref="VehicleSubtypeSpeed"/> entity configuration.
    /// </summary>
    internal sealed class VehicleSubtypeSpeedConfiguration : IEntityTypeConfiguration<VehicleSubtypeSpeed>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<VehicleSubtypeSpeed> builder)
        {
            builder.HasKey(vehicleSubtypeSpeed => vehicleSubtypeSpeed.Id);

            builder.Property(vehicleSubtypeSpeed => vehicleSubtypeSpeed.SpeedInKilometersPerHour).IsRequired();

            builder.Property(vehicleSubtypeSpeed => vehicleSubtypeSpeed.CreatedOnUtc).IsRequired();

            builder.Property(vehicleSubtypeSpeed => vehicleSubtypeSpeed.LastModifiedOnUtc).IsRequired(false);

            builder.Ignore(vehicleSubtypeSpeed => vehicleSubtypeSpeed.VehicleSubtype);
        }
    }
}
