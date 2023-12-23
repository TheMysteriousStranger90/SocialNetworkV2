using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasQueryFilter(p => p.IsApproved);

        builder.HasOne(x => x.AppUser).WithMany(x => x.Photos)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}