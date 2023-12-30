using BLL.DTOs;
using BLL.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
public class PhotosController : BaseApiController
{
    private readonly IPhotoService _photoService;

    public PhotosController(IPhotoService photoService)
    {
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

    [HttpPost]
    public async Task<ActionResult<ImageUploadResult>> AddPhoto(IFormFile file)
    {
        try
        {
            var result = await _photoService.AddPhotoAsync(file);
            return Ok(result);
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

    [HttpPut("approve/{photoId}")]
    public async Task<ActionResult> ApprovePhoto(int photoId)
    {
        try
        {
            await _photoService.ApprovePhoto(photoId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePhotoById(int id)
    {
        try
        {
            await _photoService.DeletePhotoByIdAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}