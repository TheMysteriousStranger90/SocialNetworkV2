using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasOne(rating => rating.User)
            .WithMany(user => user.Ratings)
            .HasForeignKey(rating => rating.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(rating => rating.Photo)
            .WithMany(photo => photo.Ratings)
            .HasForeignKey(rating => rating.PhotoId);
    }
}