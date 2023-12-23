using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Photo : BaseEntity
{
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public bool IsApproved { get; set; }
    public string PublicId { get; set; }
    public double AverageVote { get; set; }
    public int UserVote { get; set; }

    [ForeignKey("AppUserId")]
    public AppUser AppUser { get; set; }
    public int AppUserId { get; set; }
}