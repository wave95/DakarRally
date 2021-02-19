using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DakarRally.Application.Interfaces;
using DakarRally.Domain.Entities;
using DakarRally.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DakarRally.Persistence
{
    /// <summary>
    /// Application database context.
    /// </summary>
    public  class DakarRallyDbContext : DbContext, IDbContext
    {
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="RallySimulatorDbContext"/> class.
        /// </summary>
        /// <param name="options">The database context options.</param>
        /// <param name="dateTime">The current date and time.</param>
        /// <param name="publisher">The publisher.</param>
        public DakarRallyDbContext(DbContextOptions options, IDateTime dateTime)
            : base(options)
        {
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public new DbSet<TEntity> Set<TEntity>()
            where TEntity : Entity =>
            base.Set<TEntity>();

        /// <inheritdoc />
        public async Task<TEntity> GetBydIdAsync<TEntity>(int id)
            where TEntity : Entity
        {
            if (id <= 0)
            {
                return null;
            }

            return await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <inheritdoc />
        public void Insert<TEntity>(TEntity entity) where TEntity : Entity 
        {
            Set<TEntity>().Add(entity);
        }

        /// <inheritdoc />
        public new void Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            Set<TEntity>().Update(entity);
        }

        /// <inheritdoc />
        public new void Remove<TEntity>(TEntity entity) where TEntity : Entity
        {
            Set<TEntity>().Remove(entity);
        }
            

        /// <summary>
        /// Saves all of the pending changes in the unit of work.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The number of entities that have been saved.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DateTime utcNow = _dateTime.UtcNow;

            UpdateEntities(utcNow);

            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Updates the entities implementing <see cref="IAuditableEntity"/> interface.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        private void UpdateEntities(DateTime utcNow)
        {
            foreach (EntityEntry<Entity> entity in ChangeTracker.Entries<Entity>())
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Property(nameof(Entity.CreatedOnUtc)).CurrentValue = utcNow;
                }

                if (entity.State == EntityState.Modified)
                {
                    entity.Property(nameof(Entity.LastModifiedOnUtc)).CurrentValue = utcNow;
                }
            }
        }
    }
}
