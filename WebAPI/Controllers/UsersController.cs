using BLL.DTOs;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Specifications;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Errors;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Controllers;

public class UsersController : BaseApiController
{
    private readonly IPhotoService _photoService;
    private readonly IUserService _userService;

    public UsersController(IUserService userService,
        IPhotoService photoService)
    {
        _userService = userService;
        _photoService = photoService;
    }


    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        try
        {
            var currentUsername = User.GetUserName();
            if (currentUsername == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);

            var photoDto = _userService.AddPhotoByUser(result, currentUsername);
            var user = _userService.GetMemberAsync(User.GetUserName(), true);
            return CreatedAtAction(nameof(GetUser),
                new { username = user.Result.Username }, photoDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        try
        {
            var currentUsername = User.GetUserName();
            if (currentUsername == null) return NotFound();
            await _userService.SetMainPhotoByUser(photoId, currentUsername);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
        try
        {
            var currentUsername = User.GetUserName();
            if (currentUsername == null) return NotFound();
            await _userService.DeletePhotoByUser(photoId, currentUsername);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        try
        {
            var currentUsername = User.GetUserName();
            if (currentUsername == null) return NotFound(new ApiResponse(404));

            return await _userService.GetMemberAsync(currentUsername,
                isCurrentUser: currentUsername == username);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
    {
        try
        {
            var currentUsername = User.GetUserName();
            if (currentUsername == null) return NotFound();

            var result = await _userService.GetMembersAsync(userParams, currentUsername);
            Response.AddPaginationHeader(new PaginationHeader(result.CurrentPage, result.PageSize,
                result.TotalCount, result.TotalPages));

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{username}")]
    public async Task<ActionResult> UpdateUser(string username, AppUserDto userDto)
    {
        try
        {
            if (username != User.GetUserName()) return Unauthorized();

            await _userService.UpdateUserAsync(userDto);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{username}/friends")]
    public async Task<ActionResult<IEnumerable<AppUserDto>>> GetUserFriendsByUsername(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        if (user == null) return NotFound();

        var friends = await _userService.GetUserFriendsByIdAsync(user.Id);
        return Ok(friends);
    }

    [HttpGet("{username}/invitations")]
    public async Task<ActionResult<IEnumerable<AppUserDto>>> GetInvitationForFriendshipByUsername(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        if (user == null) return NotFound();

        var invitations = await _userService.GetInvitationForFriendshipByIdAsync(user.Id);
        return Ok(invitations);
    }

    [HttpPost("{username}/friend-request/{friendUsername}")]
    public async Task<ActionResult> SendFriendRequest(string username, string friendUsername)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        var friend = await _userService.GetUserByUsernameAsync(friendUsername);

        if (user == null || friend == null) return NotFound();

        await _userService.SendFriendRequestAsync(user.Id, friend.Id);
        return Ok();
    }

    [HttpPost("{username}/accept-friend-request/{friendUsername}")]
    public async Task<ActionResult> AcceptFriendRequest(string username, string friendUsername)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        var friend = await _userService.GetUserByUsernameAsync(friendUsername);

        if (user == null || friend == null) return NotFound();

        await _userService.AcceptFriendRequestAsync(user.Id, friend.Id);
        return Ok();
    }

    [HttpPost("{username}/reject-friend-request/{friendUsername}")]
    public async Task<ActionResult> RejectFriendRequest(string username, string friendUsername)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        var friend = await _userService.GetUserByUsernameAsync(friendUsername);

        if (user == null || friend == null) return NotFound();

        await _userService.RejectFriendRequestAsync(user.Id, friend.Id);
        return Ok();
    }

    [HttpDelete("{username}/remove-friend/{friendUsername}")]
    public async Task<ActionResult> RemoveFriend(string username, string friendUsername)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        var friend = await _userService.GetUserByUsernameAsync(friendUsername);

        if (user == null || friend == null) return NotFound();

        await _userService.RemoveFriendAsync(user.Id, friend.Id);
        return Ok();
    }
    
    [HttpPost("{username}/friends/{friendUsername}")]
    public async Task<ActionResult> AddFriend(string username, string friendUsername)
    {
        try
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            var friend = await _userService.GetUserByUsernameAsync(friendUsername);

            if (user == null || friend == null) return NotFound();

            await _userService.SendFriendRequestAsync(user.Id, friend.Id);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{username}/block/{blockedUsername}")]
    public async Task<ActionResult> BlockUser(string username, string blockedUsername)
    {
        try
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            var blockedUser = await _userService.GetUserByUsernameAsync(blockedUsername);

            if (user == null || blockedUser == null) return NotFound();

            await _userService.BlockUserAsync(user.Id, blockedUser.Id);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{username}/unblock/{unblockedUsername}")]
    public async Task<ActionResult> UnblockUser(string username, string unblockedUsername)
    {
        try
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            var unblockedUser = await _userService.GetUserByUsernameAsync(unblockedUsername);

            if (user == null || unblockedUser == null) return NotFound();

            await _userService.UnblockUserAsync(user.Id, unblockedUser.Id);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{username}/blocked")]
    public async Task<ActionResult<IEnumerable<AppUserDto>>> GetBlockedUsers(string username)
    {
        try
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null) return NotFound();

            var blockedUsers = await _userService.GetBlockedUsersAsync(user.Id);

            return Ok(blockedUsers);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{username}/is-blocked/{otherUsername}")]
    public async Task<ActionResult<bool>> IsUserBlocked(string username, string otherUsername)
    {
        try
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            var otherUser = await _userService.GetUserByUsernameAsync(otherUsername);

            if (user == null || otherUser == null) return NotFound();

            var isBlocked = await _userService.IsUserBlockedAsync(user.Id, otherUser.Id);

            return Ok(isBlocked);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}