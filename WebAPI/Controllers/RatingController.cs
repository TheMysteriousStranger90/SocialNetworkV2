using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
public class RatingController : BaseApiController
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<IEnumerable<RatingDto>>> GetRatingsByUserName(string userName)
    {
        try
        {
            var ratings = await _ratingService.GetRatingsByUserNameAsync(userName);
            return Ok(ratings);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("photo/{photoId}")]
    public async Task<ActionResult<IEnumerable<RatingDto>>> GetRatingsByPhotoId(int photoId)
    {
        try
        {
            var ratings = await _ratingService.GetRatingsByPhotoIdAsync(photoId);
            return Ok(ratings);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("photo/{photoId}/average")]
    public async Task<ActionResult<double>> GetAverageRatingForPhoto(int photoId)
    {
        try
        {
            var averageRating = await _ratingService.GetAverageRatingForPhotoAsync(photoId);
            return Ok(averageRating);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddRatingToPhoto(RatingDto ratingDto)
    {
        try
        {
            await _ratingService.AddRatingToPhotoAsync(ratingDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateRating(RatingDto ratingDto)
    {
        try
        {
            await _ratingService.UpdateRatingAsync(ratingDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}