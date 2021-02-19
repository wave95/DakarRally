using DakarRally.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DakarRally.Persistence.Configurations
{
    /// <summary>
    /// Contains the <see cref="ErrorLog"/> entity configuration.
    /// </summary>
    internal sealed class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {
            builder.HasKey(errorLog => errorLog.Id);

            builder.Property(errorLog => errorLog.HResult).IsRequired();

            builder.Property(errorLog => errorLog.Message).IsRequired();

            builder.Property(errorLog => errorLog.InnerException).IsRequired(false);

            builder.Property(errorLog => errorLog.CreatedOnUtc).IsRequired();

            builder.Property(errorLog => errorLog.LastModifiedOnUtc).IsRequired(false);


        }
    }
}
