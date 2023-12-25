using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Notification : BaseEntity
{
    public string Content { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }
    [ForeignKey("UserId")] public AppUser User { get; set; }
}