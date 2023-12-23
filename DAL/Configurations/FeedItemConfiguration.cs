using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class FeedItemConfiguration : IEntityTypeConfiguration<FeedItem>
{
    public void Configure(EntityTypeBuilder<FeedItem> builder)
    {
        builder.HasOne(fi => fi.User)
            .WithMany(u => u.FeedItems)
            .HasForeignKey(fi => fi.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}