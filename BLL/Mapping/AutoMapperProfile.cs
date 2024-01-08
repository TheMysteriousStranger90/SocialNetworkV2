using AutoMapper;
using BLL.DTOs;
using BLL.Extensions;
using DAL.Entities;

namespace BLL.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl,
                opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalcuateAge()))
            .ForMember(dest => dest.Specialization, o => o.MapFrom(src => src.Specialization.Name));

        CreateMap<AppUser, AppUserDto>()
            .ForMember(dest => dest.ThisUserFriendIds,
                opt => opt.MapFrom(src => src.ThisUserFriends.Select(item => item.AppUserFriendId)));

        CreateMap<AppUser, AppUserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<Photo, PhotoDto>();

        CreateMap<Photo, PhotoForApprovalDto>();

        CreateMap<Specialization, SpecializationDto>();

        CreateMap<RegisterDto, AppUser>();

        CreateMap<Message, MessageDto>()
            .ForMember(d => d.SenderPhotoUrl, o => o.MapFrom(s => s.Sender.Photos
                .FirstOrDefault(x => x.IsMain).Url))
            .ForMember(d => d.RecipientPhotoUrl, o => o.MapFrom(s => s.Recipient.Photos
                .FirstOrDefault(x => x.IsMain).Url));

        CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
        CreateMap<DateTime?, DateTime?>()
            .ConvertUsing(d => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);

        CreateMap<RatingDto, Rating>();

        CreateMap<Event, EventDto>()
            .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants));

        CreateMap<EventParticipant, EventParticipantDto>();

        CreateMap<FeedItem, FeedItemDto>();

        CreateMap<Notification, NotificationDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

        CreateMap<Photo, PhotoDto>()
            .ForMember(dest => dest.UserVote, opt => opt.MapFrom(src => src.UserVote))
            .ForMember(dest => dest.AverageVote, opt => opt.MapFrom(src => src.AverageVote));

        CreateMap<Rating, RatingDto>();

        CreateMap<Specialization, SpecializationDto>();

        CreateMap<MemberUpdateDto, AppUser>();

        CreateMap<UserFriends, UserFriendsDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.FriendName, opt => opt.MapFrom(src => src.AppUserFriend.UserName));
    }
}