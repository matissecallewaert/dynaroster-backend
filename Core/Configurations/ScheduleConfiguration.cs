using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(255);
            
            builder.HasMany(s => s.Shifts)
                .WithOne(shift => shift.Schedule)
                .HasForeignKey(shift => shift.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Workers)
                .WithMany(worker => worker.Schedules)
                .UsingEntity(j => j.ToTable("ScheduleWorkers"));

            builder.Property(s => s.StartDate)
                .IsRequired();

            builder.Property(s => s.EndDate)
                .IsRequired();
        }
    }
}