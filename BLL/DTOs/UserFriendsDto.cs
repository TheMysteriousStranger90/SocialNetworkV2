namespace BLL.DTOs;

public class UserFriendsDto
{
    public int? AppUserId { get; set; }
    public int? AppUserFriendId { get; set; }
    public bool IsConfirmed { get; set; }
    
    public string UserName { get; set; }
}