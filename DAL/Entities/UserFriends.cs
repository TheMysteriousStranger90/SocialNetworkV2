using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class UserFriends
{
    [ForeignKey("AppUserId")] public virtual AppUser AppUser { get; set; }
    public int? AppUserId { get; set; }

    [ForeignKey("AppUserFriendId")] public virtual AppUser AppUserFriend { get; set; }
    public int? AppUserFriendId { get; set; }

    public bool IsConfirmed { get; set; }
}