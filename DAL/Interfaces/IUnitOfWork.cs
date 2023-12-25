using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace DAL.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : class;
    IUserRepository UserRepository { get; }
    IMessageRepository MessageRepository { get; }
    ILikesRepository LikesRepository { get; }
    IPhotoRepository PhotoRepository { get; }
    IRatingRepository RatingRepository { get; }
    IAlbumRepository AlbumRepository { get; }
    IEventRepository EventRepository { get; }
    IFeedItemRepository FeedItemRepository { get; }
    IFollowRepository FollowRepository { get; }
    INotificationRepository NotificationRepository { get; }
    IUserFriendsRepository UserFriendsRepository { get; }
    IUserLikeRepository UserLikeRepository { get; }
    ISpecializationRepository SpecializationRepository { get; }
    UserManager<AppUser> UserManager { get; }
    SignInManager<AppUser> SignInManager { get; }
    RoleManager<AppRole> RoleManager { get; }
    Task<int> SaveAsync();
    bool HasChanges();
}