using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class UserFriendsConfiguration : IEntityTypeConfiguration<UserFriends>
{
    public void Configure(EntityTypeBuilder<UserFriends> builder)
    {
        builder.HasKey(u => new { u.AppUserId, u.AppUserFriendId });

        builder.HasOne(u => u.AppUser)
            .WithMany(a => a.ThisUserFriends)
            .HasForeignKey(u => u.AppUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(u => u.AppUserFriend)
            .WithMany(a => a.UserIsFriend)
            .HasForeignKey(u => u.AppUserFriendId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}