using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Rating : BaseEntity
{
    [ForeignKey("UserId")]
    public AppUser User { get; set; }
    public int UserId { get; set; }

    [ForeignKey("PhotoId")]
    public Photo Photo { get; set; }
    public int PhotoId { get; set; }

    public int Value { get; set; }
}