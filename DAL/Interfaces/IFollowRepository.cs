using DAL.Entities;

namespace DAL.Interfaces;

public interface IFollowRepository : IGenericRepository<Follow>
{
    Task<IEnumerable<AppUser>> GetFollowedUsersAsync(int userId);
    Task<IEnumerable<AppUser>> GetFollowersAsync(int userId);
    Task FollowUserAsync(int userId, int followedUserId);
    Task UnfollowUserAsync(int userId, int followedUserId);
    Task<bool> IsFollowingAsync(int userId, int otherUserId);
}