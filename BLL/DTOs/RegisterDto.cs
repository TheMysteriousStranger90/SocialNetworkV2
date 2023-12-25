using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class RegisterDto
{
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
    [Required] public DateOnly? DateOfBirth { get; set; }
    [Required] [EmailAddress] public string Email { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; }
}