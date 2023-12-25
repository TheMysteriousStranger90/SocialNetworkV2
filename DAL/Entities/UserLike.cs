using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class UserLike : BaseEntity
{
    public bool IsLike { get; set; }
    [ForeignKey("SourceUserId")] public AppUser SourceUser { get; set; }
    public int SourceUserId { get; set; }

    [ForeignKey("TargetUserId")] public AppUser TargetUser { get; set; }
    public int TargetUserId { get; set; }
}