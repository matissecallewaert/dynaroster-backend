using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.DateAdded).IsRequired();
            builder.Property(x => x.DateUpdated).IsRequired();
            builder.ToTable("Users");

            // Ensure that the base 'User' entity has a default discriminator value
            builder.HasDiscriminator<UserRole>("Role")
                .HasValue<User>(UserRole.User) // Set User as the default for User
                .HasValue<Worker>(UserRole.Worker)
                .HasValue<Manager>(UserRole.Manager);
            
            builder.Property(u => u.ProfilePicture)
                .HasColumnType("BYTEA");
            builder.HasMany(x => x.Schedules)
                .WithMany(schedule => schedule.Workers)
                .UsingEntity(j => j.ToTable("ScheduleUsers"));
        }
    }
}