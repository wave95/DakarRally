using DakarRally.Domain.Entities;
using DakarRally.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DakarRally.Persistence.Configurations
{
    /// <summary>
    /// Contains the <see cref="Vehicle"/> entity configuration.
    /// </summary>
    internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(vehicle => vehicle.Id);

            builder.Property(vehicle => vehicle.TeamName).IsRequired();

            builder.Property(vehicle => vehicle.ModelName).IsRequired();

            builder.Property(vehicle => vehicle.ManufacturingDate).HasColumnType("date").IsRequired();

            builder.Property(vehicle => vehicle.VehicleType).IsRequired();

            builder.Property(vehicle => vehicle.VehicleSubtype).IsRequired();

            builder.Property(vehicle => vehicle.Status).HasDefaultValue(VehicleStatus.Pending).IsRequired();

            builder.Property(vehicle => vehicle.HoursUntilRepaired).IsRequired(false);

            builder.Property(vehicle => vehicle.Distance).IsRequired();

            builder.Property(vehicle => vehicle.StartTimeUtc).IsRequired(false);

            builder.Property(vehicle => vehicle.HoursFromRaceStart).IsRequired(false);

            builder.Property(vehicle => vehicle.FinishTimeUtc).IsRequired(false);

            builder.Property(vehicle => vehicle.RepairmentLength).IsRequired();

            builder.Property(vehicle => vehicle.CreatedOnUtc).IsRequired();

            builder.Property(vehicle => vehicle.LastModifiedOnUtc).IsRequired(false);

            builder.HasOne<Race>()
                .WithMany()
                .HasForeignKey(vehicle => vehicle.RaceId)
                .IsRequired();

            builder.HasOne(vehicle => vehicle.Speed)
                .WithMany()
                .HasForeignKey(vehicle => vehicle.VehicleSubtypeId)
                .IsRequired();

           builder.HasOne(vehicle => vehicle.MalfunctionProbability)
               .WithMany()
               .HasForeignKey(vehicle => vehicle.VehicleSubtypeId)
               .IsRequired();


            builder.HasMany(vehicle => vehicle.Malfunctions)
                .WithOne()
                .HasForeignKey(malfunction => malfunction.VehicleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
