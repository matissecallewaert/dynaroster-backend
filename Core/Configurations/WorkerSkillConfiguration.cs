using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    public class WorkerSkillConfiguration : IEntityTypeConfiguration<WorkerSkill>
    {
        public void Configure(EntityTypeBuilder<WorkerSkill> builder)
        {
            builder.ToTable("WorkerSkills");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SkillName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ProficiencyLevel).IsRequired();
            builder.HasOne(x => x.Worker)
                .WithMany(x => x.Skills)
                .HasForeignKey(x => x.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}