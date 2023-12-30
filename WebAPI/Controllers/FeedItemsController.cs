using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
public class FeedItemsController : BaseApiController
{
    private readonly IFeedItemService _feedItemService;

    public FeedItemsController(IFeedItemService feedItemService)
    {
        _feedItemService = feedItemService;
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<IEnumerable<FeedItemDto>>> GetFeedItemsByUserName(string userName)
    {
        try
        {
            var feedItems = await _feedItemService.GetFeedItemsByUserNameAsync(userName);
            return Ok(feedItems);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("date-range")]
    public async Task<ActionResult<IEnumerable<FeedItemDto>>> GetFeedItemsInDateRange([FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        try
        {
            var feedItems = await _feedItemService.GetFeedItemsInDateRangeAsync(start, end);
            return Ok(feedItems);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}