using Core.Configurations;
using Core.Entities.Base;
using Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class WorkForceDbContext : IdentityDbContext, DbContext
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WorkForceDbContext> _logger;

        public WorkForceDbContext(
            DbContextOptions<WorkForceDbContext> options,
            IMediator mediator,
            ILogger<WorkForceDbContext> logger
        )
            : base(options)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // DbSets for Workforce Management
        public DbSet<User> Users { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<WorkerSkill> WorkerSkills { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<WorkPreference> WorkPreferences { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ShiftAssignment> ShiftAssignments { get; set; }

        public async Task<int> SaveChangesWithTimestamps(bool performAsync)
        {
            int result;
            try
            {
                var entities = ChangeTracker
                    .Entries()
                    .Where(x =>
                        x.Entity is BaseEntity
                        && (x.State == EntityState.Added || x.State == EntityState.Modified)
                    );

                foreach (var entity in entities)
                {
                    if (entity.State == EntityState.Added)
                    {
                        ((BaseEntity)entity.Entity).DateAdded = DateTime.UtcNow;
                    }

                    ((BaseEntity)entity.Entity).DateUpdated = DateTime.UtcNow;
                }

                result = performAsync ? await base.SaveChangesAsync() : base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var updates = ex
                    .Entries.SelectMany(entry =>
                        entry
                            .Properties.Where(p => p.IsModified)
                            .Select(p => $"{p.OriginalValue} -> {p.CurrentValue}")
                    )
                    .ToList();

                _logger.LogError("Concurrency issue: {Updates}", updates);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error saving data: {Message}", ex.Message);
                throw;
            }

            return result;
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default
        )
        {
            var result = await SaveChangesWithTimestamps(true);
            result += await _mediator.DispatchDomainEvents(this, _logger, false);
            result += await _mediator.DispatchDomainEvents(this, _logger, true);
            return result;
        }

        public override int SaveChanges()
        {
            var result = SaveChangesWithTimestamps(false).Result;
            result += _mediator.DispatchDomainEvents(this, _logger, false).Result;
            result += _mediator.DispatchDomainEvents(this, _logger, true).Result;
            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());
            modelBuilder.ApplyConfiguration(new ManagerConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerSkillConfiguration());
            modelBuilder.ApplyConfiguration(new AvailabilityConfiguration());
            modelBuilder.ApplyConfiguration(new WorkPreferenceConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftAssignmentConfiguration());
        }
    }
}
