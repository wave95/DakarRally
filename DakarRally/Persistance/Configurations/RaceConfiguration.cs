using DakarRally.Domain.Entities;
using DakarRally.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DakarRally.Persistence.Configurations
{
    /// <summary>
    /// Contains the <see cref="Race"/> entity configuration.
    /// </summary>
    internal sealed class RaceConfiguration : IEntityTypeConfiguration<Race>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Race> builder)
        {
            builder.HasKey(race => race.Id);

            builder.Property(race => race.Year).IsRequired();            

            builder.Property(race => race.Status).HasDefaultValue(RaceStatus.Pending).IsRequired();

            builder.Property(race => race.Length).IsRequired();

            builder.Property(race => race.StartTimeUtc).IsRequired(false);

            builder.Property(race => race.HoursFromRaceStart).IsRequired(false);

            builder.Property(race => race.EndTimeUtc).IsRequired(false);

            builder.Property(race => race.CreatedOnUtc).IsRequired();

            builder.Property(race => race.LastModifiedOnUtc).IsRequired(false);

            builder.HasMany(race => race.Vehicles)
                .WithOne()
                .HasForeignKey(vehicle => vehicle.RaceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
