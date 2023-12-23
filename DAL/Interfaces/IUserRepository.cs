using DAL.Entities;
using DAL.Specifications;

namespace DAL.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser> GetUserByIdAsync(int id);
    Task<AppUser> GetUserByUsernameAsync(string username);
    IQueryable<AppUser> GetUserByEmailAsync(string email);
    Task<AppUser> GetByEmailAsync(string email);
    Task<AppUser> GetUserByPhotoId(int photoId);
    Task<IQueryable<AppUser>> GetUserFriendsByIdAsync(int id);
    Task<IQueryable<AppUser>> GetInvitationForFriendshipByIdAsync(int id);
    IQueryable<AppUser> GetMemberAsync(string userName);
    IQueryable<AppUser> GetMembersAsync(UserParams userParams);
    Task<IEnumerable<AppUser>> GetFriendsAsync(int userId);
    Task SendFriendRequestAsync(int userId, int friendId);
    Task AcceptFriendRequestAsync(int userId, int friendId);
    Task RejectFriendRequestAsync(int userId, int friendId);
    Task RemoveFriendAsync(int userId, int friendId);
    Task BlockUserAsync(int userId, int blockedUserId);
    Task UnblockUserAsync(int userId, int unblockedUserId);
    Task<bool> IsUserBlockedAsync(int userId, int otherUserId);
    Task<IEnumerable<AppUser>> GetBlockedUsersAsync(int userId);
}