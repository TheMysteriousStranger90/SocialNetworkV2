using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Specifications;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserLikeRepository : GenericRepository<UserLike>, IUserLikeRepository
{
    public UserLikeRepository(SocialNetworkContext context) : base(context)
    {
    }

    public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
    {
        return await _context.Likes.FindAsync(sourceUserId, targetUserId);
    }

    public async Task<AppUser> GetUserWithLikes(int userId)
    {
        return await _context.Users
            .Include(x => x.LikedUsers)
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public IQueryable<AppUser> GetUserLikes(LikesParams likesParams)
    {
        var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
        var likes = _context.Likes.AsQueryable();

        if (likesParams.Predicate == "liked")
        {
            likes = likes.Where(like => like.SourceUserId == likesParams.AppUserId);
            users = likes.Select(like => like.TargetUser);
        }

        if (likesParams.Predicate == "likedBy")
        {
            likes = likes.Where(like => like.TargetUserId == likesParams.AppUserId);
            users = likes.Select(like => like.SourceUser);
        }
        
        return users;
    }

    public async Task<IEnumerable<UserLike>> GetLikesByUserIdAsync(int userId)
    {
        return await _context.Likes
            .Where(ul => ul.SourceUserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserLike>> GetUsersWhoLikedUserAsync(int userId)
    {
        return await _context.Likes
            .Where(ul => ul.TargetUserId == userId)
            .ToListAsync();
    }

    public async Task<bool> UserLikesUserAsync(int sourceUserId, int targetUserId)
    {
        return await _context.Likes
            .AnyAsync(ul => ul.SourceUserId == sourceUserId && ul.TargetUserId == targetUserId);
    }

    public async Task AddLikeAsync(int sourceUserId, int targetUserId)
    {
        var userLike = new UserLike
        {
            SourceUserId = sourceUserId,
            TargetUserId = targetUserId,
            IsLike = true
        };

        await _context.Likes.AddAsync(userLike);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveLikeAsync(int sourceUserId, int targetUserId)
    {
        var userLike = await _context.Likes
            .FirstOrDefaultAsync(ul => ul.SourceUserId == sourceUserId && ul.TargetUserId == targetUserId);

        if (userLike != null)
        {
            _context.Likes.Remove(userLike);
            await _context.SaveChangesAsync();
        }
    }
}