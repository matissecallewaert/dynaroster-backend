using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.ToTable("Shifts");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ShiftDate).IsRequired();
            builder.Property(x => x.StartTime).IsRequired();
            builder.Property(x => x.EndTime).IsRequired();
            builder.Property(x => x.NeededWorkers).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.ScheduleId).IsRequired();
            builder.HasOne(x => x.Schedule)
                .WithMany(x => x.Shifts)
                .HasForeignKey(x => x.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Location)
                .WithOne()
                .HasForeignKey<Shift>(x => x.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Assignments)
                .WithOne()
                .HasForeignKey(x => x.ShiftId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}