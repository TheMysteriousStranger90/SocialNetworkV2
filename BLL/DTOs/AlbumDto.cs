namespace BLL.DTOs;

public class AlbumDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<PhotoDto> Photos { get; set; }
}