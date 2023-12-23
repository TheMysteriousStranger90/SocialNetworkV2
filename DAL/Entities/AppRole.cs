using Microsoft.AspNetCore.Identity;

namespace DAL.Entities;

public class AppRole : IdentityRole<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}