using DAL.Entities;

namespace DAL.Interfaces;

public interface IUserFriendsRepository : IGenericRepository<UserFriends>
{
    Task<IEnumerable<UserFriends>> GetFriendsByUserIdAsync(int userId);
    Task<IEnumerable<UserFriends>> GetFriendRequestsByUserIdAsync(int userId);
    Task<bool> AreUsersFriendsAsync(int userId, int friendId);
    Task SendFriendRequestAsync(int userId, int friendId);
    Task AcceptFriendRequestAsync(int userId, int friendId);
    Task RejectFriendRequestAsync(int userId, int friendId);
    Task RemoveFriendAsync(int userId, int friendId);
}