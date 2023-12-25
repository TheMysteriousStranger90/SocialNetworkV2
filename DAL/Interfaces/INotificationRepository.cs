using DAL.Entities;

namespace DAL.Interfaces;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(int userId);
    Task<IEnumerable<Notification>> GetUnreadNotificationsByUserIdAsync(int userId);
    Task MarkAsReadAsync(int notificationId);
    Task MarkAllAsReadAsync(int userId);
    Task CreatePhotoToAlbumNotificationAsync(int albumId, Photo photo);
}