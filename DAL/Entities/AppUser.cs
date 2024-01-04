using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities;

public class AppUser : IdentityUser<int>
{
    public DateOnly DateOfBirth { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Gender { get; set; }
    public string Introduction { get; set; }
    public string LookingFor { get; set; }
    public string Interests { get; set; }
    public bool ProfileVisibility { get; set; }
    public string RelationshipStatus { get; set; }
    public string Education { get; set; }
    public string Work { get; set; }
    public virtual Specialization Specialization { get; set; }
    public int? SpecializationId { get; set; }
    public List<Photo> Photos { get; set; } = new();
    public ICollection<AppUserRole> UserRoles { get; set; }
    public ICollection<UserBlock> BlockedByUsers { get; set; }
    public ICollection<UserBlock> BlockedUsers { get; set; }
    public ICollection<UserFriends> UserIsFriend { get; set; }
    public ICollection<UserFriends> ThisUserFriends { get; set; }
    public ICollection<UserLike> LikedByUsers { get; set; }
    public ICollection<UserLike> LikedUsers { get; set; }
    public ICollection<Message> MessagesSent { get; set; }
    public ICollection<Message> MessagesReceived { get; set; }
    public ICollection<Event> Events { get; set; }
    public ICollection<FeedItem> FeedItems { get; set; }
    public ICollection<Follow> Following { get; set; }
    public ICollection<Follow> Followers { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<EventParticipant> EventsParticipated { get; set; }
    


    public AppUser()
    {
        UserIsFriend ??= new HashSet<UserFriends>();
        ThisUserFriends ??= new HashSet<UserFriends>();
        BlockedByUsers ??= new HashSet<UserBlock>();
        BlockedUsers ??= new HashSet<UserBlock>();
        LikedByUsers = new HashSet<UserLike>();
        LikedUsers = new HashSet<UserLike>();
        MessagesSent = new HashSet<Message>();
        MessagesReceived = new HashSet<Message>();
        Events = new HashSet<Event>();
        FeedItems = new HashSet<FeedItem>();
        Following = new HashSet<Follow>();
        Followers = new HashSet<Follow>();
        Ratings = new HashSet<Rating>();
        EventsParticipated = new HashSet<EventParticipant>();
    }
}