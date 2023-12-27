using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Helpers;
using BLL.Interfaces;
using CloudinaryDotNet.Actions;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Specifications;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _photoService = photoService;
    }
    
    public async Task<PhotoDto> AddPhotoByUser(ImageUploadResult result, string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);

        if (user == null) throw new SocialNetworkException("Not Found!");
        
        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };
        
        user.Photos.Add(photo);

        await _unitOfWork.SaveAsync();

        return _mapper.Map<PhotoDto>(photo);
    }

  public async Task UpdateUserAsync(AppUserDto userDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userDto.UserName);
        _mapper.Map(userDto, user);
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task<AppUserDto> GetUserByIdAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
        return _mapper.Map<AppUserDto>(user);
    }

    public async Task<IEnumerable<AppUserDto>> GetUsersAsync()
    {
        var users = await _unitOfWork.UserRepository.GetUsersAsync();
        return _mapper.Map<IEnumerable<AppUserDto>>(users);
    }

    public async Task<AppUserDto> GetUserByUsernameAsync(string username)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
        return _mapper.Map<AppUserDto>(user);
    }

    public async Task<AppUserDto> GetUserByEmailAsync(string email)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(email);
        return _mapper.Map<AppUserDto>(user);
    }

    public async Task<AppUserDto> GetUserByPhotoId(int photoId)
    {
        var user = await _unitOfWork.UserRepository.GetUserByPhotoId(photoId);
        return _mapper.Map<AppUserDto>(user);
    }

    public async Task<IEnumerable<AppUserDto>> GetUserFriendsByIdAsync(int id)
    {
        var friends = await _unitOfWork.UserRepository.GetFriendsAsync(id);
        return _mapper.Map<IEnumerable<AppUserDto>>(friends);
    }

    public async Task<IEnumerable<AppUserDto>> GetInvitationForFriendshipByIdAsync(int id)
    {
        var invitations = await _unitOfWork.UserRepository.GetInvitationForFriendshipByIdAsync(id);
        return _mapper.Map<IEnumerable<AppUserDto>>(invitations);
    }

    public async Task<IEnumerable<AppUserDto>> GetFriendsAsync(int userId)
    {
        var friends = await _unitOfWork.UserRepository.GetFriendsAsync(userId);
        return _mapper.Map<IEnumerable<AppUserDto>>(friends);
    }

    public async Task SendFriendRequestAsync(int userId, int friendId)
    {
        await _unitOfWork.UserRepository.SendFriendRequestAsync(userId, friendId);
        await _unitOfWork.SaveAsync();
    }

    public async Task AcceptFriendRequestAsync(int userId, int friendId)
    {
        await _unitOfWork.UserRepository.AcceptFriendRequestAsync(userId, friendId);
        await _unitOfWork.SaveAsync();
    }

    public async Task RejectFriendRequestAsync(int userId, int friendId)
    {
        await _unitOfWork.UserRepository.RejectFriendRequestAsync(userId, friendId);
        await _unitOfWork.SaveAsync();
    }

    public async Task RemoveFriendAsync(int userId, int friendId)
    {
        await _unitOfWork.UserRepository.RemoveFriendAsync(userId, friendId);
        await _unitOfWork.SaveAsync();
    }

    public async Task BlockUserAsync(int userId, int blockedUserId)
    {
        await _unitOfWork.UserRepository.BlockUserAsync(userId, blockedUserId);
        await _unitOfWork.SaveAsync();
    }

    public async Task UnblockUserAsync(int userId, int unblockedUserId)
    {
        await _unitOfWork.UserRepository.UnblockUserAsync(userId, unblockedUserId);
        await _unitOfWork.SaveAsync();
    }

    public async Task<bool> IsUserBlockedAsync(int userId, int otherUserId)
    {
        return await _unitOfWork.UserRepository.IsUserBlockedAsync(userId, otherUserId);
    }

    public async Task<IEnumerable<AppUserDto>> GetBlockedUsersAsync(int userId)
    {
        var blockedUsers = await _unitOfWork.UserRepository.GetBlockedUsersAsync(userId);
        return _mapper.Map<IEnumerable<AppUserDto>>(blockedUsers);
    }

    public async Task<Pagination<MemberDto>> GetMembersAsync(UserParams userParams, string userName)
    {
        var currentUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        userParams.CurrentUsername = currentUser.UserName;
        
        if (string.IsNullOrEmpty(userParams.Gender))
        {
            userParams.Gender = currentUser.Gender == "male" ? "female" : "male";
        }

        var query = _unitOfWork.UserRepository.GetMembersAsync(userParams);
        
        return await Pagination<MemberDto>.CreateAsync(
            query.AsNoTracking().ProjectTo<MemberDto>(_mapper.ConfigurationProvider), 
            userParams.PageNumber, 
            userParams.PageSize);
    }

    public async Task<IEnumerable<AppUserDto>> GetMembersAsync(UserParams userParams)
    {
        var users = _unitOfWork.UserRepository.GetMembersAsync(userParams);
        return _mapper.Map<IEnumerable<AppUserDto>>(users);
    }

    public async Task<MemberDto> GetMemberAsync(string username, bool isCurrentUser)
    {
        var query = _unitOfWork.UserRepository.GetMemberAsync(username);
        var result = query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsQueryable();

        if (isCurrentUser) result = result.IgnoreQueryFilters();

        return await result.FirstOrDefaultAsync();
    }

    public async Task<MemberDto> GetMemberByIdAsync(int id)
    {
        var query = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
        var user = _mapper.Map<MemberDto>(query);

        return user;
    }

    public async Task SetMainPhotoByUser(int photoId, string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);

        if (user == null) throw new SocialNetworkException("Not Found!");

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        if (photo == null) throw new SocialNetworkException("Not Found!");

        if (photo.IsMain) throw new SocialNetworkException("This is already your main photo!");

        var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
        if (currentMain != null) currentMain.IsMain = false;
        photo.IsMain = true;

        await _unitOfWork.SaveAsync();
    }

    public async Task DeletePhotoByUser(int photoId, string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);

        var photo = await _unitOfWork.PhotoRepository.GetPhotoByIdAsync(photoId);

        if (photo == null) throw new SocialNetworkException("Not Found!");

        if (photo.IsMain) throw new SocialNetworkException("You cannot delete your main photo");

        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) throw new SocialNetworkException("Error!");
        }

        user.Photos.Remove(photo);

        await _unitOfWork.SaveAsync();
    }
}