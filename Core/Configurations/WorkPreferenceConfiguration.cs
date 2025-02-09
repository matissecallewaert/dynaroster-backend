using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    public class WorkPreferenceConfiguration : IEntityTypeConfiguration<WorkPreference>
    {
        public void Configure(EntityTypeBuilder<WorkPreference> builder)
        {
            builder.ToTable("WorkPreferences");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PreferredJob).IsRequired().HasMaxLength(100);
            builder.HasOne(x => x.Worker)
                .WithMany(x => x.Preferences)
                .HasForeignKey(x => x.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}