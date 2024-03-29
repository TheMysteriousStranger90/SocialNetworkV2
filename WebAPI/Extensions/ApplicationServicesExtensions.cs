﻿using BLL.Helpers;
using BLL.Interfaces;
using BLL.Mapping;
using BLL.Services;
using DAL;
using DAL.Context;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebAPI.SignalR;

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
        
        services.AddSignalR();
        services.AddSingleton<PresenceTracker>();
        
        return services;
    }
}