using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers;

[Authorize]
public class NotificationsController : BaseApiController
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("{userName}")]
    public async Task<ActionResult> SendNotification(string userName, [FromBody] string content)
    {
        try
        {
            await _notificationService.SendNotificationAsync(userName, content);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{userName}/read/{notificationId}")]
    public async Task<ActionResult> MarkAsRead(string userName, int notificationId)
    {
        try
        {
            await _notificationService.MarkAsReadAsync(userName, notificationId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{userName}/read-all")]
    public async Task<ActionResult> MarkAsReadAll(string userName)
    {
        try
        {
            await _notificationService.MarkAsReadAllAsync(userName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Cached(600)]
    [HttpGet("{userName}")]
    public async Task<ActionResult<IEnumerable<NotificationDto>>> GetNotificationsByUserId(string userName)
    {
        try
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userName);
            return Ok(notifications);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Cached(600)]
    [HttpGet("{userName}/unread")]
    public async Task<ActionResult<IEnumerable<NotificationDto>>> GetUnreadNotificationsByUserId(string userName)
    {
        try
        {
            var notifications = await _notificationService.GetUnreadNotificationsByUserIdAsync(userName);
            return Ok(notifications);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("photo-notification/{userName}")]
    public async Task<ActionResult> CreatePhotoNotification(string userName)
    {
        try
        {
            await _notificationService.CreatePhotoToAlbumNotificationAsync(userName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}