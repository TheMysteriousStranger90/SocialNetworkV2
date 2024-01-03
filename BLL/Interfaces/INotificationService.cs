using BLL.DTOs;

namespace BLL.Interfaces;

public interface INotificationService
{
    Task SendNotificationAsync(string userName, string content);
    Task MarkAsReadAsync(string userName, int notificationId);
    Task MarkAsReadAllAsync(string userName);
    Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(string userName);
    Task<IEnumerable<NotificationDto>> GetUnreadNotificationsByUserIdAsync(string userName);
    Task CreatePhotoToAlbumNotificationAsync(PhotoDto photoDto);
}