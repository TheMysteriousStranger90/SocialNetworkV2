using DAL.Entities;
using DAL.Specifications;

namespace DAL.Interfaces;

public interface ILikesRepository
{
    Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
    Task<AppUser> GetUserWithLikes(int userId);
    IQueryable<AppUser> GetUserLikes(LikesParams likesParams);
}