using BLL.DTOs;
using BLL.Helpers;
using CloudinaryDotNet.Actions;
using DAL.Specifications;

namespace BLL.Interfaces;

public interface IUserService
{
    Task UpdateUserAsync(MemberUpdateDto memberUpdateDto, string userName);
    Task<AppUserDto> GetUserByIdAsync(int id);
    Task<IEnumerable<AppUserDto>> GetUsersAsync();
    Task<AppUserDto> GetUserByUsernameAsync(string username);
    Task<AppUserDto> GetUserByEmailAsync(string email);
    Task<AppUserDto> GetUserByPhotoId(int photoId);
    Task<PhotoDto> AddPhotoByUser(ImageUploadResult result, string userName);
    Task<IEnumerable<AppUserDto>> GetUserFriendsByIdAsync(int id);
    Task<IEnumerable<AppUserDto>> GetInvitationForFriendshipByIdAsync(int id);
    Task<MemberDto> GetMemberAsync(string userName, bool isCurrentUser);
    Task<MemberDto> GetMemberByIdAsync(int id);
    Task<Pagination<MemberDto>> GetMembersAsync(UserParams userParams, string userName);
    Task<IEnumerable<AppUserDto>> GetMembersAsync(UserParams userParams);
    Task<IEnumerable<AppUserDto>> GetFriendsAsync(int userId);
    Task SendFriendRequestAsync(int userId, int friendId);
    Task AcceptFriendRequestAsync(int userId, int friendId);
    Task RejectFriendRequestAsync(int userId, int friendId);
    Task RemoveFriendAsync(int userId, int friendId);
    Task BlockUserAsync(int userId, int blockedUserId);
    Task UnblockUserAsync(int userId, int unblockedUserId);
    Task<bool> IsUserBlockedAsync(int userId, int otherUserId);
    Task<IEnumerable<AppUserDto>> GetBlockedUsersAsync(int userId);
    Task SetMainPhotoByUser(int photoId, string userName);
    Task DeletePhotoByUser(int photoId, string userName);
}