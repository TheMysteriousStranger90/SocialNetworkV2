using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task SendNotificationAsync(string userName, string content)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var notification = new Notification
        {
            Content = content,
            CreatedAt = DateTime.UtcNow,
            UserId = user.Id,
            IsRead = false
        };
        _unitOfWork.NotificationRepository.Add(notification);
    }

    public async Task MarkAsReadAsync(string userName, int notificationId)
    {
        await _unitOfWork.NotificationRepository.MarkAsReadAsync(notificationId);
    }

    public async Task MarkAsReadAllAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        await _unitOfWork.NotificationRepository.MarkAllAsReadAsync(user.Id);
    }

    public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var notifications = await _unitOfWork.NotificationRepository.GetNotificationsByUserIdAsync(user.Id);
        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    public async Task<IEnumerable<NotificationDto>> GetUnreadNotificationsByUserIdAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var notifications = await _unitOfWork.NotificationRepository.GetUnreadNotificationsByUserIdAsync(user.Id);
        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    public async Task CreatePhotoToAlbumNotificationAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        await _unitOfWork.NotificationRepository.CreatePhotoToAlbumNotificationAsync(user);
    }
}