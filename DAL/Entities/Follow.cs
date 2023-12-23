using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Follow : BaseEntity
{
    [ForeignKey("FollowerId")] public AppUser Follower { get; set; }
    public int FollowerId { get; set; }

    [ForeignKey("FollowedId")] public AppUser Followed { get; set; }
    public int FollowedId { get; set; }
}