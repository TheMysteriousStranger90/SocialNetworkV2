using AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Extensions;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Specifications;

namespace BLL.Services;

public class LikeService : ILikeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LikeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddLike(string userName, int sourceUserId)
    {
        var likedUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var sourceUser = await _unitOfWork.LikesRepository.GetUserWithLikes(sourceUserId);

        if (likedUser == null) throw new SocialNetworkException("Not Found");

        if (sourceUser.UserName == userName) throw new SocialNetworkException("You cannot like yourself");

        var userLike = await _unitOfWork.LikesRepository.GetUserLike(sourceUserId, likedUser.Id);

        if (userLike != null) throw new SocialNetworkException("You already like this user");

        userLike = new UserLike
        {
            SourceUserId = sourceUserId,
            TargetUserId = likedUser.Id
        };

        sourceUser.LikedUsers.Add(userLike);
        
        await _unitOfWork.SaveAsync();
    }

    public async Task<Pagination<LikeDto>> GetUserLikes(LikesParams likesParams)
    {
        var users = _unitOfWork.LikesRepository.GetUserLikes(likesParams);
        
        var likedUsers = users.Select(user => new LikeDto
        {
            Username = user.UserName,
            Age = user.DateOfBirth.CalcuateAge(),
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
            City = user.City,
            Id = user.Id
        });
        return await Pagination<LikeDto>.CreateAsync(likedUsers, likesParams.PageNumber, likesParams.PageSize);
    }
}