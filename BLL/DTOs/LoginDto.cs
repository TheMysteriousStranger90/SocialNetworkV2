using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class LoginDto
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Please, enter valid email address")]
    [StringLength(40, ErrorMessage = "Must be between 5 and 40 characters", MinimumLength = 5)]
    [EmailAddress]
    public string Email { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "Password is required")]
    [StringLength(30, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}