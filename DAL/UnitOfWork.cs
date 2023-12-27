using System.Collections;
using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DAL;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly SocialNetworkContext _context;
    private Hashtable _repositories;
    private IUserRepository _userRepository;
    private IMessageRepository _messageRepository;
    private ILikesRepository _likesRepository;
    private IPhotoRepository _photoRepository;
    private ISpecializationRepository _specializationRepository;
    private IRatingRepository _ratingRepository;
    private IEventRepository _eventRepository;
    private IFeedItemRepository _feedItemRepository;
    private IFollowRepository _followRepository;
    private INotificationRepository _notificationRepository;
    private IUserFriendsRepository _userFriendsRepository;
    private IUserLikeRepository _userLikeRepository;

    public UnitOfWork(SocialNetworkContext context, RoleManager<AppRole> roleManager,
        UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        if (_repositories == null) _repositories = new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (GenericRepository<T>)_repositories[type];
    }

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

    public IMessageRepository MessageRepository => _messageRepository ??= new MessageRepository(_context);

    public ILikesRepository LikesRepository => _likesRepository ??= new LikesRepository(_context);

    public IPhotoRepository PhotoRepository => _photoRepository ??= new PhotoRepository(_context);

    public IRatingRepository RatingRepository => _ratingRepository ??= new RatingRepository(_context);
    
    public ISpecializationRepository SpecializationRepository => _specializationRepository ??= new SpecializationRepository(_context);
    
    public IEventRepository EventRepository => _eventRepository ??= new EventRepository(_context);

    public IFeedItemRepository FeedItemRepository => _feedItemRepository ??= new FeedItemRepository(_context);

    public IFollowRepository FollowRepository => _followRepository ??= new FollowRepository(_context);

    public INotificationRepository NotificationRepository => _notificationRepository ??= new NotificationRepository(_context);

    public IUserFriendsRepository UserFriendsRepository => _userFriendsRepository ??= new UserFriendsRepository(_context);

    public IUserLikeRepository UserLikeRepository => _userLikeRepository ??= new UserLikeRepository(_context);

    private readonly UserManager<AppUser> _userManager;

    public UserManager<AppUser> UserManager
    {
        get { return _userManager; }
    }

    private readonly SignInManager<AppUser> _signInManager;

    public SignInManager<AppUser> SignInManager
    {
        get { return _signInManager; }
    }

    private readonly RoleManager<AppRole> _roleManager;

    public RoleManager<AppRole> RoleManager
    {
        get { return _roleManager; }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}