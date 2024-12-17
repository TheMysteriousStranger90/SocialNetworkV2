using BLL.Helpers;
using BLL.Interfaces;
using BLL.Mapping;
using BLL.Services;
using DAL;
using DAL.Context;
using DAL.Entities;
using DAL.Helpers;
using DAL.Interfaces;
using DAL.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebAPI.Filters;
using WebAPI.Services;
using WebAPI.Services.Interfaces;
using WebAPI.SignalR;
using WebAPI.Validators;

namespace WebAPI.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        
        services.AddDbContext<SocialNetworkContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
        
        services.AddSingleton<IConnectionMultiplexer>(c => 
        {
            var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
            return ConnectionMultiplexer.Connect(options);
        });
        
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        services.AddAutoMapper(typeof(AutoMapperProfile));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IFeedItemService, FeedItemService>();
        services.AddScoped<IFollowService, FollowService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IUserFriendsService, UserFriendsService>();
        
        services.AddSingleton<IRemoteHostService, RemoteHostService>();
        services.AddSingleton<IIpBlockingService, IpBlockingService>();
        
        services.AddScoped<IpBlockActionFilter>();
        
        services.AddTransient<IPasswordHasher<AppUser>>(provider =>
        {
            var innerHasher = new PasswordHasher<AppUser>();
            var logger = provider.GetRequiredService<ILogger<LoggingPasswordHasher<AppUser>>>();
            return new LoggingPasswordHasher<AppUser>(innerHasher, logger);
        });
        
        services.AddSignalR();
        services.AddSingleton<PresenceTracker>();
        
        services.AddValidatorsFromAssemblyContaining<MessageValidator>();
        
        services.AddLogging();
        
        return services;
    }
}