using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
public class FollowController : BaseApiController
{
    private readonly IFollowService _followService;

    public FollowController(IFollowService followService)
    {
        _followService = followService;
    }

    [HttpGet("{userName}/followed")]
    public async Task<ActionResult<IEnumerable<AppUserDto>>> GetFollowedUsers(string userName)
    {
        try
        {
            var followedUsers = await _followService.GetFollowedUsersAsync(userName);
            return Ok(followedUsers);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userName}/followers")]
    public async Task<ActionResult<IEnumerable<AppUserDto>>> GetFollowers(string userName)
    {
        try
        {
            var followers = await _followService.GetFollowersAsync(userName);
            return Ok(followers);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{userName}/follow/{followedUserName}")]
    public async Task<ActionResult> FollowUser(string userName, string followedUserName)
    {
        try
        {
            await _followService.FollowUserAsync(userName, followedUserName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{userName}/unfollow/{followedUserName}")]
    public async Task<ActionResult> UnfollowUser(string userName, string followedUserName)
    {
        try
        {
            await _followService.UnfollowUserAsync(userName, followedUserName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userName}/is-following/{otherUserName}")]
    public async Task<ActionResult<bool>> IsFollowing(string userName, string otherUserName)
    {
        try
        {
            var isFollowing = await _followService.IsFollowingAsync(userName, otherUserName);
            return Ok(isFollowing);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}