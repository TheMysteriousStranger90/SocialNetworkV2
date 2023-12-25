﻿namespace BLL.DTOs;

public class AppUserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
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
    public virtual ICollection<int> ThisUserFriendIds { get; set; }
    public virtual ICollection<int> ThisBlockedUsersIds { get; set; }
}