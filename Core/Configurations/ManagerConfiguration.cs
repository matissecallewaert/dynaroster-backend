using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.ToTable("Managers");
            builder.HasMany(x => x.Workers)
                .WithOne()
                .HasForeignKey(x => x.Id) // Manager manages workers via their IDs
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}