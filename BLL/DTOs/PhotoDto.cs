namespace BLL.DTOs;

public class PhotoDto
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public bool IsApproved { get; set; }
    public double AverageVote { get; set; }
    public int UserVote { get; set; }
}