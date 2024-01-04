using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Specifications;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SocialNetworkContext _context;

    public UserRepository(SocialNetworkContext context)
    {
        _context = context;
    }

    public void Update(AppUser user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _context.Users.Include(x => x.Photos)
            .ToListAsync();
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<AppUser> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.Email == email);
    }

    public IQueryable<AppUser> GetUserByEmailAsync(string email)
    {
        var query = _context.Users
            .Where(x => x.Email == email)
            .Include(p => p.Photos)
            .AsQueryable();

        return query;
    }

    public async Task<AppUser> GetUserByPhotoId(int photoId)
    {
        return await _context.Users
            .Include(p => p.Photos)
            .IgnoreQueryFilters()
            .Where(p => p.Photos.Any(p => p.Id == photoId))
            .FirstOrDefaultAsync();
    }

    public async Task<IQueryable<AppUser>> GetUserFriendsByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        var friends = Enumerable.Empty<AppUser>().AsQueryable();
        if (user != null && user.ThisUserFriends.Count != 0)
        {
            foreach (var friendship in user.ThisUserFriends)
            {
                if (friendship.IsConfirmed)
                {
                    var friend = await _context.Users.FindAsync(friendship);
                    friends.Append(friend);
                }
            }
        }

        return friends;
    }

    public async Task<IQueryable<AppUser>> GetInvitationForFriendshipByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        var friends = Enumerable.Empty<AppUser>().AsQueryable();
        if (user != null && user.ThisUserFriends.Count != 0)
        {
            foreach (var friendship in user.ThisUserFriends)
            {
                if (!friendship.IsConfirmed)
                {
                    var friend = await _context.Users.FindAsync(friendship);
                    friends.Append(friend);
                }
            }
        }

        return friends;
    }

    public IQueryable<AppUser> GetMemberAsync(string userName)
    {
        var query = _context.Users
            .Where(x => x.UserName == userName)
            .AsQueryable();

        return query;
    }

    public IQueryable<AppUser> GetMembersAsync(UserParams userParams)
    {
        var query = _context.Users.AsQueryable();

        query = query.Where(u => u.UserName != userParams.CurrentUsername);
        query = query.Where(u => u.Gender == userParams.Gender);

        var minDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MaxAge - 1));
        var maxDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MinAge));

        query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

        if (!string.IsNullOrEmpty(userParams.Search))
        {
            query = query.Where(u => u.LastName.Contains(userParams.Search));
        }

        query = userParams.OrderBy switch
        {
            "created" => query.OrderByDescending(u => u.Created),
            _ => query.OrderByDescending(u => u.LastActive)
        };

        return query;
    }

    public async Task<IEnumerable<AppUser>> GetFriendsAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user.ThisUserFriends.Select(f => f.AppUserFriend).ToList();
    }

    public async Task SendFriendRequestAsync(int userId, int friendId)
    {
        var user = await _context.Users.FindAsync(userId);
        var friend = await _context.Users.FindAsync(friendId);
        user.ThisUserFriends.Add(new UserFriends { AppUserFriend = friend });
        await _context.SaveChangesAsync();
    }

    public async Task AcceptFriendRequestAsync(int userId, int friendId)
    {
        var user = await _context.Users.FindAsync(userId);
        var friend = user.ThisUserFriends.FirstOrDefault(f => f.AppUserFriendId == friendId);
        if (friend != null)
        {
            friend.IsConfirmed = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task RejectFriendRequestAsync(int userId, int friendId)
    {
        var user = await _context.Users.FindAsync(userId);
        var friend = user.ThisUserFriends.FirstOrDefault(f => f.AppUserFriendId == friendId);
        if (friend != null)
        {
            user.ThisUserFriends.Remove(friend);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveFriendAsync(int userId, int friendId)
    {
        var user = await _context.Users.FindAsync(userId);
        var friend = user.ThisUserFriends.FirstOrDefault(f => f.AppUserFriendId == friendId);
        if (friend != null)
        {
            user.ThisUserFriends.Remove(friend);
            await _context.SaveChangesAsync();
        }
    }

    public async Task BlockUserAsync(int userId, int blockedUserId)
    {
        var userBlock = new UserBlock
        {
            SourceUserId = userId,
            BlockedUserId = blockedUserId
        };

        await _context.UserBlocks.AddAsync(userBlock);
        await _context.SaveChangesAsync();
    }

    public async Task UnblockUserAsync(int userId, int unblockedUserId)
    {
        var userBlock = await _context.UserBlocks
            .FirstOrDefaultAsync(ub => ub.SourceUserId == userId && ub.BlockedUserId == unblockedUserId);

        if (userBlock != null)
        {
            _context.UserBlocks.Remove(userBlock);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsUserBlockedAsync(int userId, int otherUserId)
    {
        return await _context.UserBlocks
            .AnyAsync(ub => ub.SourceUserId == userId && ub.BlockedUserId == otherUserId);
    }

    public async Task<IEnumerable<AppUser>> GetBlockedUsersAsync(int userId)
    {
        return await _context.UserBlocks
            .Where(ub => ub.SourceUserId == userId)
            .Select(ub => ub.BlockedUser)
            .ToListAsync();
    }
}