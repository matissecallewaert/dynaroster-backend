using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasMany(x => x.Workers)
                .WithOne(x => x.Manager)
                .HasForeignKey(x => x.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}