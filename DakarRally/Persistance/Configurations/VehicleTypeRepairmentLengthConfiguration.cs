using DakarRally.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DakarRally.Persistence.Configurations
{
    /// <summary>
    /// Contains the <see cref="VehicleTypeRepairmentLength"/> entity configuration.
    /// </summary>
    internal sealed class VehicleTypeRepairmentLengthConfiguration : IEntityTypeConfiguration<VehicleTypeRepairmentLength>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<VehicleTypeRepairmentLength> builder)
        {
            builder.HasKey(vehicleTypeRepairmentLength => vehicleTypeRepairmentLength.Id);

            builder.Property(vehicleTypeRepairmentLength => vehicleTypeRepairmentLength.RepairmentLengthInHours).IsRequired();

            builder.Ignore(vehicleTypeRepairmentLength => vehicleTypeRepairmentLength.VehicleType);
        }
    }
}
