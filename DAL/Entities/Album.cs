using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Album : BaseEntity
{
    [ForeignKey("UserId")]
    public AppUser User { get; set; }
    public int UserId { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<Photo> Photos { get; set; }
}