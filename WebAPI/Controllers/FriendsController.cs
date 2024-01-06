using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
public class FriendsController : BaseApiController
{
    private readonly IUserFriendsService _userFriendsService;

    public FriendsController(IUserFriendsService userFriendsService)
    {
        _userFriendsService = userFriendsService;
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<IEnumerable<string>>> GetFriendsByUserName(string userName)
    {
        try
        {
            var friends = await _userFriendsService.GetFriendsByUserNameAsync(userName);
            return Ok(friends);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userName}/requests")]
    public async Task<ActionResult<IEnumerable<UserFriendsDto>>> GetFriendRequestsByUserName(string userName)
    {
        try
        {
            var friendRequests = await _userFriendsService.GetFriendRequestsByUserNameAsync(userName);
            return Ok(friendRequests);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userName}/are-friends/{friendName}")]
    public async Task<ActionResult<bool>> AreUsersFriends(string userName, string friendName)
    {
        try
        {
            var areFriends = await _userFriendsService.AreUsersFriendsAsync(userName, friendName);
            return Ok(areFriends);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{userName}/requests/{friendName}")]
    public async Task<ActionResult> SendFriendRequest(string userName, string friendName)
    {
        try
        {
            await _userFriendsService.SendFriendRequestAsync(userName, friendName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{userName}/requests/{friendName}/accept")]
    public async Task<ActionResult> AcceptFriendRequest(string userName, string friendName)
    {
        try
        {
            await _userFriendsService.AcceptFriendRequestAsync(userName, friendName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{userName}/requests/{friendName}/reject")]
    public async Task<ActionResult> RejectFriendRequest(string userName, string friendName)
    {
        try
        {
            await _userFriendsService.RejectFriendRequestAsync(userName, friendName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{userName}/friends/{friendName}")]
    public async Task<ActionResult> RemoveFriend(string userName, string friendName)
    {
        try
        {
            await _userFriendsService.RemoveFriendAsync(userName, friendName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}