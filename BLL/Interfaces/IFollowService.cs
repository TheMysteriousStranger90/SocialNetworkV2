using BLL.DTOs;

namespace BLL.Interfaces;

public interface IFollowService
{
    Task<IEnumerable<AppUserDto>> GetFollowedUsersAsync(string userName);
    Task<IEnumerable<AppUserDto>> GetFollowersAsync(string userName);
    Task FollowUserAsync(string userName, string followedUserName);
    Task UnfollowUserAsync(string userName, string followedUserName);
    Task<bool> IsFollowingAsync(string userName, string otherUserName);
}