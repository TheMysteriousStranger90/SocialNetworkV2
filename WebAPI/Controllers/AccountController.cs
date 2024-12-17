using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Filters;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers;

/// <summary>
/// Controller handling user account operations
/// </summary>
[ServiceFilter(typeof(IpBlockActionFilter))]
public class AccountController : BaseApiController
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAuthService _authService;
    private readonly IRemoteHostService _remoteHostService;
    private readonly IIpBlockingService _ipBlockingService;

    public AccountController(ILogger<AccountController> logger, IAuthService authService, IRemoteHostService remoteHostService, IIpBlockingService ipBlockingService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _remoteHostService = remoteHostService ?? throw new ArgumentNullException(nameof(remoteHostService));
        _ipBlockingService = ipBlockingService ?? throw new ArgumentNullException(nameof(ipBlockingService));
    }

    /// <summary>
    /// Registers a new user
    /// </summary>
    [HttpPost("register")]
    [SwaggerOperation(Summary = "Register new user", Description = "Creates a new user account")]
    [ProducesResponseType(typeof(AppUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AppUserDto>> Register(RegisterDto registerDto)
    {
        var clientIp = _remoteHostService.GetRemoteHostIpAddressUsingRemoteIpAddress(HttpContext) ??
                       _remoteHostService.GetRemoteHostIpAddressUsingXForwardedFor(HttpContext) ??
                       _remoteHostService.GetRemoteHostIpAddressUsingXRealIp(HttpContext);

        if (clientIp != null && _ipBlockingService.IsBlocked(clientIp))
        {
            _logger.LogWarning("Blocked IP attempted to register: {ClientIp}", clientIp);
            return StatusCode(StatusCodes.Status403Forbidden, "Your IP address is blocked.");
        }

        _logger.LogInformation("Registering new user: {Email} from IP: {ClientIp}", registerDto.Email, clientIp);
        try
        {
            var user = await _authService.RegisterAsync(registerDto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering user: {Email} from IP: {ClientIp}", registerDto.Email, clientIp);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Authenticates a user
    /// </summary>
    [HttpPost("login")]
    [SwaggerOperation(Summary = "Login user", Description = "Authenticates user credentials")]
    [ProducesResponseType(typeof(AppUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AppUserDto>> Login(LoginDto loginDto)
    {
        var clientIp = _remoteHostService.GetRemoteHostIpAddressUsingRemoteIpAddress(HttpContext) ??
                       _remoteHostService.GetRemoteHostIpAddressUsingXForwardedFor(HttpContext) ??
                       _remoteHostService.GetRemoteHostIpAddressUsingXRealIp(HttpContext);

        if (clientIp != null && _ipBlockingService.IsBlocked(clientIp))
        {
            _logger.LogWarning("Blocked IP attempted to login: {ClientIp}", clientIp);
            return StatusCode(StatusCodes.Status403Forbidden, "Your IP address is blocked.");
        }

        _logger.LogInformation("Logging in user: {Email} from IP: {ClientIp}", loginDto.Email, clientIp);
        try
        {
            var user = await _authService.LoginAsync(loginDto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error logging in user: {Email} from IP: {ClientIp}", loginDto.Email, clientIp);
            return Unauthorized(ex.Message);
        }
    }

    [HttpPost("external-login")]
    public async Task<ActionResult<AppUserDto>> GoogleLogin([FromBody] ExternalAuthDto externalAuthn)
    {
        try
        {
            var result = await _authService.GoogleLoginAsync(externalAuthn);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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

    /// <summary>
    /// Retrieves the current logged-in user
    /// </summary>
    [Authorize]
    [HttpGet]
    [SwaggerOperation(Summary = "Get current user", Description = "Retrieves the current logged-in user")]
    [ProducesResponseType(typeof(AppUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCurrentUser()
    {
        var clientIp = _remoteHostService.GetRemoteHostIpAddressUsingRemoteIpAddress(HttpContext) ??
                       _remoteHostService.GetRemoteHostIpAddressUsingXForwardedFor(HttpContext) ??
                       _remoteHostService.GetRemoteHostIpAddressUsingXRealIp(HttpContext);

        _logger.LogInformation("Getting current user from IP: {ClientIp}", clientIp);
        try
        {
            var user = await _authService.FindByEmailFromClaims(User);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting current user from IP: {ClientIp}", clientIp);
            return BadRequest(ex.Message);
        }
    }
}