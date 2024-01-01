namespace BLL.DTOs;

public class NotificationDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
}