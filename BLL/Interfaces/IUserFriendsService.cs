using BLL.DTOs;

namespace BLL.Interfaces;

public interface IUserFriendsService
{
    Task<IEnumerable<UserFriendsDto>> GetFriendsByUserNameAsync(string userName);
    Task<IEnumerable<UserFriendsDto>> GetFriendRequestsByUserNameAsync(string userName);
    Task<bool> AreUsersFriendsAsync(string userName, string friendName);
    Task SendFriendRequestAsync(string userName, string friendName);
    Task AcceptFriendRequestAsync(string userName, string friendName);
    Task RejectFriendRequestAsync(string userName, string friendName);
    Task RemoveFriendAsync(string userName, string friendName);
}