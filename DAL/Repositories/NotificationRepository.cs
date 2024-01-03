using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    public NotificationRepository(SocialNetworkContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(int userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Notification>> GetUnreadNotificationsByUserIdAsync(int userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .ToListAsync();
    }

    public async Task MarkAsReadAsync(int notificationId)
    {
        var notification = await _context.Notifications.FindAsync(notificationId);
        if (notification != null)
        {
            notification.IsRead = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        var notifications = _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead);
        foreach (var notification in notifications)
        {
            notification.IsRead = true;
        }
        await _context.SaveChangesAsync();
    }

    public async Task CreatePhotoToAlbumNotificationAsync(Photo photo)
    {
        var notification = new Notification
        {
            Content = $"A new photo has been added {photo.AppUser.UserName}",
            CreatedAt = DateTime.UtcNow,
            UserId = photo.AppUserId,
            IsRead = false
        };
        await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync();
    }
}