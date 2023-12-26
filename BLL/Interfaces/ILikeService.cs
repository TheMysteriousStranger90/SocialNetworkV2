using BLL.DTOs;
using BLL.Helpers;
using DAL.Specifications;

namespace BLL.Interfaces;

public interface ILikeService
{
    Task AddLike(string userName, int sourceUserId);
    Task<Pagination<LikeDto>> GetUserLikes(LikesParams likesParams);
}