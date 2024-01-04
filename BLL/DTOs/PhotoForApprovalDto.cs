namespace BLL.DTOs;

public class PhotoForApprovalDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Url { get; set; }
    public bool IsApproved { get; set; }
}