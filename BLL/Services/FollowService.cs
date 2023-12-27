using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services;

public class FollowService : IFollowService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FollowService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<AppUserDto>> GetFollowedUsersAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var followedUsers = await _unitOfWork.FollowRepository.GetFollowedUsersAsync(user.Id);
        return _mapper.Map<IEnumerable<AppUserDto>>(followedUsers);
    }

    public async Task<IEnumerable<AppUserDto>> GetFollowersAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var followers = await _unitOfWork.FollowRepository.GetFollowersAsync(user.Id);
        return _mapper.Map<IEnumerable<AppUserDto>>(followers);
    }

    public async Task FollowUserAsync(string userName, string followedUserName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var followedUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(followedUserName);
        await _unitOfWork.FollowRepository.FollowUserAsync(user.Id, followedUser.Id);
    }

    public async Task UnfollowUserAsync(string userName, string followedUserName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var followedUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(followedUserName);
        await _unitOfWork.FollowRepository.UnfollowUserAsync(user.Id, followedUser.Id);
    }

    public async Task<bool> IsFollowingAsync(string userName, string otherUserName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var otherUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(otherUserName);
        return await _unitOfWork.FollowRepository.IsFollowingAsync(user.Id, otherUser.Id);
    }
}