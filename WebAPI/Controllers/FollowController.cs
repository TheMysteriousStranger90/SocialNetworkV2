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
        var followedUsers = await _followService.GetFollowedUsersAsync(userName);
        return Ok(followedUsers);
    }

    [HttpGet("{userName}/followers")]
    public async Task<ActionResult<IEnumerable<AppUserDto>>> GetFollowers(string userName)
    {
        var followers = await _followService.GetFollowersAsync(userName);
        return Ok(followers);
    }

    [HttpPost("{userName}/follow/{followedUserName}")]
    public async Task<ActionResult> FollowUser(string userName, string followedUserName)
    {
        await _followService.FollowUserAsync(userName, followedUserName);
        return Ok();
    }

    [HttpDelete("{userName}/unfollow/{followedUserName}")]
    public async Task<ActionResult> UnfollowUser(string userName, string followedUserName)
    {
        await _followService.UnfollowUserAsync(userName, followedUserName);
        return Ok();
    }

    [HttpGet("{userName}/is-following/{otherUserName}")]
    public async Task<ActionResult<bool>> IsFollowing(string userName, string otherUserName)
    {
        var isFollowing = await _followService.IsFollowingAsync(userName, otherUserName);
        return Ok(isFollowing);
    }
}