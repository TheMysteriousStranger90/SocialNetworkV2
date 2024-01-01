using System.Security.Claims;
using AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AuthService(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenService = tokenService;
    }


    public async Task<AppUserDto> FindByEmailFromClaims(object ob)
    {
        var claimsPrincipal = ob as ClaimsPrincipal;
        var email = claimsPrincipal?.FindFirstValue(ClaimTypes.Email);

        if (email == null)
        {
            throw new Exception("Invalid claims principal");
        }

        var user = await _unitOfWork.UserManager.FindByEmailAsync(email);

        return new AppUserDto
        {
            Email = user.Email,
            Token = await _tokenService.CreateToken(user),
            Username = user.UserName,
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
            Gender = user.Gender,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };
    }

    public async Task<AppUserDto> RegisterAsync(RegisterDto registerDto)
    {
        if (await CheckUserNameExistsAsync(registerDto.Username)) throw new SocialNetworkException("UserName is taken");
        if (await CheckEmailExistsAsync(registerDto.Email)) throw new SocialNetworkException("Email is taken");

        var user = _mapper.Map<AppUser>(registerDto);
        
        user.SpecializationId = 1;
        user.ProfileVisibility = true;
        user.UserName = registerDto.Username.ToLower();

        var result = await _unitOfWork.UserManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) throw new SocialNetworkException("Error!");

        var roleResult = await _unitOfWork.UserManager.AddToRoleAsync(user, "Member");

        if (!roleResult.Succeeded) throw new SocialNetworkException("Error!");

        return new AppUserDto
        {
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Token = await _tokenService.CreateToken(user),
            Gender = user.Gender,
            Country = user.Country,
            City = user.City,
            ProfileVisibility = true
        };
    }

    public async Task<AppUserDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _unitOfWork.UserManager.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.Email == loginDto.Email);

        if (user == null) throw new SocialNetworkException("Invalid email");

        var result = await _unitOfWork.SignInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) throw new SocialNetworkException("Invalid password");

        return new AppUserDto
        {
            Email = user.Email,
            Token = await _tokenService.CreateToken(user),
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
            Gender = user.Gender
        };
    }

    public async Task<bool> CheckUserNameExistsAsync(string userName)
    {
        if (userName == null) throw new SocialNetworkException("UserName doesn't exist");
        return await _unitOfWork.UserManager.Users.AnyAsync(x => x.UserName == userName.ToLower());
    }

    public async Task<bool> CheckEmailExistsAsync(string email)
    {
        if (email == null) throw new SocialNetworkException("Email doesn't exist");
        return await _unitOfWork.UserManager.FindByEmailAsync(email) != null;
    }

    public async Task<bool> ConfirmEmailAsync(string userName, string token)
    {
        var user = await _unitOfWork.UserManager.FindByNameAsync(userName);
        if (user == null) throw new SocialNetworkException("User doesn't exist");
        var result = await _unitOfWork.UserManager.ConfirmEmailAsync(user, token);
        return result.Succeeded;
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(string userName)
    {
        var user = await _unitOfWork.UserManager.FindByNameAsync(userName);
        if (user == null) throw new SocialNetworkException("User doesn't exist");
        return await _unitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<bool> ResetPasswordAsync(string userName, string token, string newPassword)
    {
        var user = await _unitOfWork.UserManager.FindByNameAsync(userName);
        if (user == null) throw new SocialNetworkException("User doesn't exist");
        var result = await _unitOfWork.UserManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded;
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string userName)
    {
        var user = await _unitOfWork.UserManager.FindByNameAsync(userName);
        if (user == null) throw new SocialNetworkException("User doesn't exist");
        return await _unitOfWork.UserManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<bool> ChangePasswordAsync(string userName, string currentPassword, string newPassword)
    {
        var user = await _unitOfWork.UserManager.FindByNameAsync(userName);
        if (user == null) throw new SocialNetworkException("User doesn't exist");
        var result = await _unitOfWork.UserManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result.Succeeded;
    }
}