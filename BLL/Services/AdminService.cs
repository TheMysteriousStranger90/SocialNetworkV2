using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<object>> GetUsersWithRoles()
    {
        var users = await _unitOfWork.UserManager.Users
            .OrderBy(u => u.UserName)
            .Select(u => new
            {
                u.Id,
                Username = u.UserName,
                Email = u.Email,
                Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
            })
            .ToListAsync();

        return users;
    }

    public async Task<IEnumerable<string>> EditRoles(string username, string roles)
    {
        if (string.IsNullOrEmpty(roles)) throw new SocialNetworkException("You must select at least one role");

        var selectedRoles = roles.Split(",").ToArray();

        var user = await _unitOfWork.UserManager.FindByNameAsync(username);

        if (user == null) throw new SocialNetworkException("Not Found!");

        var userRoles = await _unitOfWork.UserManager.GetRolesAsync(user);

        var result = await _unitOfWork.UserManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

        if (!result.Succeeded) throw new SocialNetworkException("Failed to add to roles");

        result = await _unitOfWork.UserManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

        if (!result.Succeeded) throw new SocialNetworkException("Failed to remove from roles");

        var tmp = await _unitOfWork.UserManager.GetRolesAsync(user);

        return tmp;
    }

    public async Task<IEnumerable<object>> GetRoles()
    {
        var roles = await _unitOfWork.RoleManager.Roles
            .Select(r => new
            {
                r.Id,
                Name = r.Name
            })
            .ToListAsync();

        return roles;
    }

    public async Task<bool> AddRole(string roleName)
    {
        if (string.IsNullOrEmpty(roleName)) throw new SocialNetworkException("Role name cannot be empty");

        var roleExists = await _unitOfWork.RoleManager.RoleExistsAsync(roleName);
        if (roleExists) throw new SocialNetworkException("Role already exists");

        var result = await _unitOfWork.RoleManager.CreateAsync(new AppRole { Name = roleName });

        return result.Succeeded;
    }

    public async Task<bool> DeleteRole(string roleName)
    {
        var role = await _unitOfWork.RoleManager.FindByNameAsync(roleName);
        if (role == null) throw new SocialNetworkException("Role not found");

        var result = await _unitOfWork.RoleManager.DeleteAsync(role);

        return result.Succeeded;
    }

    public async Task<bool> LockUser(string userName)
    {
        var user = await _unitOfWork.UserManager.FindByNameAsync(userName);
        if (user == null) throw new SocialNetworkException("User not found");

        user.LockoutEnd = DateTime.Now.AddYears(100);
        var result = await _unitOfWork.UserManager.UpdateAsync(user);

        return result.Succeeded;
    }

    public async Task<bool> UnlockUser(string userName)
    {
        var user = await _unitOfWork.UserManager.FindByNameAsync(userName);
        if (user == null) throw new SocialNetworkException("User not found");

        user.LockoutEnd = null;
        var result = await _unitOfWork.UserManager.UpdateAsync(user);

        return result.Succeeded;
    }
}