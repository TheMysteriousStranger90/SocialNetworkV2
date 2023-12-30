using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
public class AdminController : BaseApiController
{
    private readonly IAdminService _adminService;
    private readonly IPhotoService _photoService;

    public AdminController(IAdminService adminService,
        IPhotoService photoService)
    {
        _photoService = photoService;
        _adminService = adminService;
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("users-with-roles")]
    public async Task<ActionResult> GetUsersWithRoles()
    {
        try
        {
            var users = await _adminService.GetUsersWithRoles();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("edit-roles/{username}")]
    public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
    {
        try
        {
            var result = await _adminService.EditRoles(username, roles);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("roles")]
    public async Task<ActionResult> GetRoles()
    {
        try
        {
            var roles = await _adminService.GetRoles();
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("add-role")]
    public async Task<ActionResult> AddRole(string roleName)
    {
        try
        {
            var result = await _adminService.AddRole(roleName);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("delete-role")]
    public async Task<ActionResult> DeleteRole(string roleName)
    {
        try
        {
            var result = await _adminService.DeleteRole(roleName);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpGet("photos-to-moderate")]
    public async Task<ActionResult> GetPhotosForModeration()
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

    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpPost("approve-photo/{photoId}")]
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

    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpPost("reject-photo/{photoId}")]
    public async Task<ActionResult> RejectPhoto(int photoId)
    {
        try
        {
            await _photoService.DeletePhotoByIdAsync(photoId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("lock-user/{userName}")]
    public async Task<ActionResult> LockUser(string userName)
    {
        try
        {
            var result = await _adminService.LockUser(userName);
            if (result)
            {
                return Ok("User locked successfully");
            }
            else
            {
                return BadRequest("Failed to lock user");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("unlock-user/{userName}")]
    public async Task<ActionResult> UnlockUser(string userName)
    {
        try
        {
            var result = await _adminService.UnlockUser(userName);
            if (result)
            {
                return Ok("User unlocked successfully");
            }
            else
            {
                return BadRequest("Failed to unlock user");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}