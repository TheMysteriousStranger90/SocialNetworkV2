using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class FollowRepository : GenericRepository<Follow>, IFollowRepository
{
    public FollowRepository(SocialNetworkContext context) : base(context)
    {
    }

    public async Task<IEnumerable<AppUser>> GetFollowedUsersAsync(int userId)
    {
        return await _context.Follows
            .Where(f => f.FollowerId == userId)
            .Select(f => f.Followed)
            .ToListAsync();
    }

    public async Task<IEnumerable<AppUser>> GetFollowersAsync(int userId)
    {
        return await _context.Follows
            .Where(f => f.FollowedId == userId)
            .Select(f => f.Follower)
            .ToListAsync();
    }

    public async Task FollowUserAsync(int userId, int followedUserId)
    {
        var follow = new Follow
        {
            FollowerId = userId,
            FollowedId = followedUserId
        };

        _context.Follows.Add(follow);
        await _context.SaveChangesAsync();
    }

    public async Task UnfollowUserAsync(int userId, int followedUserId)
    {
        var follow = await _context.Follows
            .FirstOrDefaultAsync(f => f.FollowerId == userId && f.FollowedId == followedUserId);

        if (follow != null)
        {
            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsFollowingAsync(int userId, int otherUserId)
    {
        return await _context.Follows
            .AnyAsync(f => f.FollowerId == userId && f.FollowedId == otherUserId);
    }
}