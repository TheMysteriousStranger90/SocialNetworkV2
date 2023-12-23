using DAL.Entities;
using DAL.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DateOnlyConverter = System.ComponentModel.DateOnlyConverter;

namespace DAL.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);

        builder.Property(a => a.LastName).IsRequired().HasMaxLength(50);

        builder.HasOne(a => a.Specialization).WithMany()
            .HasForeignKey(a => a.SpecializationId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(a => a.UserRoles).WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.Property(x => x.DateOfBirth)
            .HasConversion(new DateOnlyToDateTimeConverter());
    }
}