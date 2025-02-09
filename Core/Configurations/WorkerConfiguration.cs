using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.HasMany(x => x.Skills)
                .WithOne()
                .HasForeignKey(x => x.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Availabilities)
                .WithOne()
                .HasForeignKey(x => x.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Preferences)
                .WithOne()
                .HasForeignKey(x => x.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}