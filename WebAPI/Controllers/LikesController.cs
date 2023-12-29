using BLL.DTOs;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Controllers;

[Authorize]
public class LikesController : BaseApiController
{
    private readonly ILikeService _likeService;

    public LikesController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpPost("{username}")]
    public async Task<ActionResult> AddLike(string userName)
    {
        var sourceUserId = User.GetUserId();
        await _likeService.AddLike(userName, sourceUserId);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<LikeDto>>> GetUserLikes([FromQuery] LikesParams likesParams)
    {
        likesParams.AppUserId = User.GetUserId();

        var users = await _likeService.GetUserLikes(likesParams);

        Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage,
            users.PageSize, users.TotalCount, users.TotalPages));

        return Ok(users);
    }
}