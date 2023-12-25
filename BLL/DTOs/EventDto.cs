namespace BLL.DTOs;

public class EventDto
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime EventDate { get; set; }
    public string Location { get; set; }
    public List<EventParticipantDto> Participants { get; set; }
}