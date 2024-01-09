using BLL.DTOs;
using BLL.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

[Authorize]
public class PhotosController : BaseApiController
{
    private readonly IPhotoService _photoService;
    private readonly IUserService _userService;

    public PhotosController(IUserService userService,
        IPhotoService photoService)
    {
        _userService = userService;
        _photoService = photoService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PhotoDto>> GetPhotoById(int id)
    {
        try
        {
            var photo = await _photoService.GetPhotoByIdAsync(id);
            return Ok(photo);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("unapproved")]
    public async Task<ActionResult<IEnumerable<PhotoForApprovalDto>>> GetUnapprovedPhotos()
    {
        try
        {
            var photos = await _photoService.GetUnapprovedPhotos();
            return Ok(photos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}