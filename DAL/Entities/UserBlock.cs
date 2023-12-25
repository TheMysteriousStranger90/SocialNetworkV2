namespace DAL.Entities;

public class UserBlock
{
    public int SourceUserId { get; set; }
    public AppUser SourceUser { get; set; }

    public int BlockedUserId { get; set; }
    public AppUser BlockedUser { get; set; }
}