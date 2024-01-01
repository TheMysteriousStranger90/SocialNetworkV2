namespace BLL.DTOs;

public class LikeDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public int Age { get; set; }
    public string PhotoUrl { get; set; }
    public string City { get; set; }
    public bool IsLike { get; set; }
    public int SourceUserId { get; set; }
    public int TargetUserId { get; set; }
}