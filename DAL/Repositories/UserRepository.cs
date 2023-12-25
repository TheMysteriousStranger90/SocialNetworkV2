using DAL.Entities;
using DAL.Interfaces;
using DAL.Specifications;

namespace DAL.Repositories;

public class UserRepository : IUserRepository
{
    public void Update(AppUser user)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> GetUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public IQueryable<AppUser> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> GetUserByPhotoId(int photoId)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<AppUser>> GetUserFriendsByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<AppUser>> GetInvitationForFriendshipByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<AppUser> GetMemberAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public IQueryable<AppUser> GetMembersAsync(UserParams userParams)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AppUser>> GetFriendsAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task SendFriendRequestAsync(int userId, int friendId)
    {
        throw new NotImplementedException();
    }

    public Task AcceptFriendRequestAsync(int userId, int friendId)
    {
        throw new NotImplementedException();
    }

    public Task RejectFriendRequestAsync(int userId, int friendId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFriendAsync(int userId, int friendId)
    {
        throw new NotImplementedException();
    }

    public Task BlockUserAsync(int userId, int blockedUserId)
    {
        throw new NotImplementedException();
    }

    public Task UnblockUserAsync(int userId, int unblockedUserId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserBlockedAsync(int userId, int otherUserId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AppUser>> GetBlockedUsersAsync(int userId)
    {
        throw new NotImplementedException();
    }
}