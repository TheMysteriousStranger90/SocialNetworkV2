using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserFriendsRepository : GenericRepository<UserFriends>, IUserFriendsRepository
{
    public UserFriendsRepository(SocialNetworkContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserFriends>> GetFriendsByUserIdAsync(int userId)
    {
        return await _context.UserFriends
            .Where(uf => uf.AppUserId == userId && uf.IsConfirmed)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserFriends>> GetFriendRequestsByUserIdAsync(int userId)
    {
        return await _context.UserFriends
            .Where(uf => uf.AppUserFriendId == userId && !uf.IsConfirmed)
            .ToListAsync();
    }

    public async Task<bool> AreUsersFriendsAsync(int userId, int friendId)
    {
        return await _context.UserFriends
            .AnyAsync(uf => uf.AppUserId == userId && uf.AppUserFriendId == friendId && uf.IsConfirmed);
    }

    public async Task SendFriendRequestAsync(int userId, int friendId)
    {
        var userFriend = new UserFriends
        {
            AppUserId = userId,
            AppUserFriendId = friendId,
            IsConfirmed = false
        };

        await _context.UserFriends.AddAsync(userFriend);
        await _context.SaveChangesAsync();
    }

    public async Task AcceptFriendRequestAsync(int userId, int friendId)
    {
        var userFriend = await _context.UserFriends
            .FirstOrDefaultAsync(uf => uf.AppUserId == friendId && uf.AppUserFriendId == userId && !uf.IsConfirmed);

        if (userFriend != null)
        {
            userFriend.IsConfirmed = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task RejectFriendRequestAsync(int userId, int friendId)
    {
        var userFriend = await _context.UserFriends
            .FirstOrDefaultAsync(uf => uf.AppUserId == friendId && uf.AppUserFriendId == userId && !uf.IsConfirmed);

        if (userFriend != null)
        {
            _context.UserFriends.Remove(userFriend);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveFriendAsync(int userId, int friendId)
    {
        var userFriend = await _context.UserFriends
            .FirstOrDefaultAsync(uf => uf.AppUserId == userId && uf.AppUserFriendId == friendId && uf.IsConfirmed);

        if (userFriend != null)
        {
            _context.UserFriends.Remove(userFriend);
            await _context.SaveChangesAsync();
        }
    }
}