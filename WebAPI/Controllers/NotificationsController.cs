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
        await _notificationService.SendNotificationAsync(userName, content);
        return Ok();
    }

    [HttpPut("{userName}/read/{notificationId}")]
    public async Task<ActionResult> MarkAsRead(string userName, int notificationId)
    {
        await _notificationService.MarkAsReadAsync(userName, notificationId);
        return Ok();
    }

    [HttpPut("{userName}/read-all")]
    public async Task<ActionResult> MarkAsReadAll(string userName)
    {
        await _notificationService.MarkAsReadAllAsync(userName);
        return Ok();
    }

    [Cached(600)]
    [HttpGet("{userName}")]
    public async Task<ActionResult<IEnumerable<NotificationDto>>> GetNotificationsByUserId(string userName)
    {
        var notifications = await _notificationService.GetNotificationsByUserIdAsync(userName);
        return Ok(notifications);
    }

    [Cached(600)]
    [HttpGet("{userName}/unread")]
    public async Task<ActionResult<IEnumerable<NotificationDto>>> GetUnreadNotificationsByUserId(string userName)
    {
        var notifications = await _notificationService.GetUnreadNotificationsByUserIdAsync(userName);
        return Ok(notifications);
    }

    [HttpPost("photo-notification/{albumId}")]
    public async Task<ActionResult> CreatePhotoToAlbumNotification(int albumId, [FromBody] PhotoDto photoDto)
    {
        await _notificationService.CreatePhotoToAlbumNotificationAsync(albumId, photoDto);
        return Ok();
    }
}