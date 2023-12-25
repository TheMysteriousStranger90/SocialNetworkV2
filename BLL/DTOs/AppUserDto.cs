namespace BLL.DTOs;

public class AppUserDto
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Gender { get; set; }
    public string Introduction { get; set; }
    public string LookingFor { get; set; }
    public string Interests { get; set; }
    public string RelationshipStatus { get; set; }
    public string Education { get; set; }
    public string Work { get; set; }
    public bool ProfileVisibility { get; set; }
    public string Token { get; set; }
    public virtual ICollection<int> ThisUserFriendIds { get; set; }
    public virtual ICollection<int> ThisBlockedUsersIds { get; set; }
}