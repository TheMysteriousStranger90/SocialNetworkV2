namespace BLL.Interfaces;

public interface IAdminService
{
    Task<IEnumerable<object>> GetUsersWithRoles();
    Task<IEnumerable<string>> EditRoles(string userName, string roles);
    Task<IEnumerable<object>> GetRoles();
    Task<bool> AddRole(string roleName);
    Task<bool> DeleteRole(string roleName);
    Task<bool> LockUser(string userName);
    Task<bool> UnlockUser(string userName);
}