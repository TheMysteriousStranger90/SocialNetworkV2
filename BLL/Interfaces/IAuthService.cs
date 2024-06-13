using BLL.DTOs;

namespace BLL.Interfaces;

public interface IAuthService
{
    Task<AppUserDto> FindByEmailFromClaims(object ob);
    Task<AppUserDto> RegisterAsync(RegisterDto registerDto);
    Task<AppUserDto> LoginAsync(LoginDto loginDto);
    Task<AppUserDto>  GoogleLoginAsync(ExternalAuthDto externalAuthn);
    Task<bool> CheckUserNameExistsAsync(string userName);
    Task<bool> CheckEmailExistsAsync(string email);
    Task<bool> ConfirmEmailAsync(string userName, string token);
    Task<string> GenerateEmailConfirmationTokenAsync(string userName);
    Task<bool> ResetPasswordAsync(string userName, string token, string newPassword);
    Task<string> GeneratePasswordResetTokenAsync(string userName);
    Task<bool> ChangePasswordAsync(string userName, string currentPassword, string newPassword);
}