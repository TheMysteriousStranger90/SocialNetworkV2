using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class EventParticipant : BaseEntity
{
    [ForeignKey("EventId")]
    public Event Event { get; set; }
    public int EventId { get; set; }

    [ForeignKey("UserId")]
    public AppUser User { get; set; }
    public int UserId { get; set; }
}