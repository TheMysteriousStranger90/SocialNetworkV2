using DAL.Entities;
using DAL.Specifications;

namespace DAL.Interfaces;

public interface IUserLikeRepository : IGenericRepository<UserLike>
{
    Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
    Task<AppUser> GetUserWithLikes(int userId);
    IQueryable<AppUser> GetUserLikes(LikesParams likesParams);
    Task<IEnumerable<UserLike>> GetLikesByUserIdAsync(int userId);
    Task<IEnumerable<UserLike>> GetUsersWhoLikedUserAsync(int userId);
    Task<bool> UserLikesUserAsync(int sourceUserId, int targetUserId);
    Task AddLikeAsync(int sourceUserId, int targetUserId);
    Task RemoveLikeAsync(int sourceUserId, int targetUserId);
}