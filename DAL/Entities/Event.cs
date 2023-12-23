using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Event : BaseEntity
{
    [ForeignKey("UserId")] public AppUser User { get; set; }
    public int UserId { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime EventDate { get; set; }
    public string Location { get; set; }

    public ICollection<EventParticipant> Participants { get; set; }
}