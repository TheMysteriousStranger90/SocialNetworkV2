using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class FeedItem : BaseEntity
{
    [ForeignKey("UserId")] public AppUser User { get; set; }
    public int UserId { get; set; }

    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}