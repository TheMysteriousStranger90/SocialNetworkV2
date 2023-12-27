using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services;

public class UserFriendsService : IUserFriendsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserFriendsService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<UserFriendsDto>> GetFriendsByUserNameAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var friends = await _unitOfWork.UserFriendsRepository.GetFriendsByUserIdAsync(user.Id);
        return _mapper.Map<IEnumerable<UserFriendsDto>>(friends);
    }

    public async Task<IEnumerable<UserFriendsDto>> GetFriendRequestsByUserNameAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var friendRequests = await _unitOfWork.UserFriendsRepository.GetFriendRequestsByUserIdAsync(user.Id);
        return _mapper.Map<IEnumerable<UserFriendsDto>>(friendRequests);
    }

    public async Task<bool> AreUsersFriendsAsync(string userName, string friendName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var friend = await _unitOfWork.UserRepository.GetUserByUsernameAsync(friendName);
        return await _unitOfWork.UserFriendsRepository.AreUsersFriendsAsync(user.Id, friend.Id);
    }

    public async Task SendFriendRequestAsync(string userName, string friendName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var friend = await _unitOfWork.UserRepository.GetUserByUsernameAsync(friendName);
        await _unitOfWork.UserFriendsRepository.SendFriendRequestAsync(user.Id, friend.Id);
    }

    public async Task AcceptFriendRequestAsync(string userName, string friendName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var friend = await _unitOfWork.UserRepository.GetUserByUsernameAsync(friendName);
        await _unitOfWork.UserFriendsRepository.AcceptFriendRequestAsync(user.Id, friend.Id);
    }

    public async Task RejectFriendRequestAsync(string userName, string friendName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var friend = await _unitOfWork.UserRepository.GetUserByUsernameAsync(friendName);
        await _unitOfWork.UserFriendsRepository.RejectFriendRequestAsync(user.Id, friend.Id);
    }

    public async Task RemoveFriendAsync(string userName, string friendName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var friend = await _unitOfWork.UserRepository.GetUserByUsernameAsync(friendName);
        await _unitOfWork.UserFriendsRepository.RemoveFriendAsync(user.Id, friend.Id);
    }
}