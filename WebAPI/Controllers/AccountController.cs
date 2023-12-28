using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class AccountController : BaseApiController
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AppUserDto>> Register(RegisterDto registerDto)
    {
        try
        {
            var user = await _authService.RegisterAsync(registerDto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AppUserDto>> Login(LoginDto loginDto)
    {
        try
        {
            var user = await _authService.LoginAsync(loginDto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
    
    [HttpPost("confirm-email")]
    public async Task<ActionResult<bool>> ConfirmEmail(string userName, string token)
    {
        try
        {
            var result = await _authService.ConfirmEmailAsync(userName, token);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<bool>> ResetPassword(string userName, string token, string newPassword)
    {
        try
        {
            var result = await _authService.ResetPasswordAsync(userName, token, newPassword);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("change-password")]
    public async Task<ActionResult<bool>> ChangePassword(string userName, string currentPassword, string newPassword)
    {
        try
        {
            var result = await _authService.ChangePasswordAsync(userName, currentPassword, newPassword);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        try
        {
            return await _authService.CheckEmailExistsAsync(email);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("usernameexists")]
    public async Task<ActionResult<bool>> CheckUserNameExistsAsync([FromQuery] string userName)
    {
        try
        {
            return await _authService.CheckUserNameExistsAsync(userName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<AppUserDto>> GetCurrentUser()
    {
        return await _authService.FindByEmailFromClaims(User);
    }
}