using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class UserBlockConfiguration : IEntityTypeConfiguration<UserBlock>
{
    public void Configure(EntityTypeBuilder<UserBlock> builder)
    {
        builder
            .HasKey(ub => new { ub.SourceUserId, ub.BlockedUserId });

        builder
            .HasOne(ub => ub.SourceUser)
            .WithMany(u => u.BlockedUsers)
            .HasForeignKey(ub => ub.SourceUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(ub => ub.BlockedUser)
            .WithMany(u => u.BlockedByUsers)
            .HasForeignKey(ub => ub.BlockedUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}