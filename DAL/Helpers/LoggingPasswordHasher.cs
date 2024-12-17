using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DAL.Helpers;

public class LoggingPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
{
    private readonly IPasswordHasher<TUser> _innerHasher;
    private readonly ILogger<LoggingPasswordHasher<TUser>> _logger;

    public LoggingPasswordHasher(IPasswordHasher<TUser> innerHasher, ILogger<LoggingPasswordHasher<TUser>> logger)
    {
        _innerHasher = innerHasher ?? throw new ArgumentNullException(nameof(innerHasher));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public string HashPassword(TUser user, string password)
    {
        _logger.LogInformation("Hashing password for user: {User}", user);
        var hashedPassword = _innerHasher.HashPassword(user, password);
        _logger.LogInformation("Password hashed for user: {User}", user);
        return hashedPassword;
    }

    public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
    {
        _logger.LogInformation("Verifying hashed password for user: {User}", user);
        var result = _innerHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        _logger.LogInformation("Password verification result for user: {User} is {Result}", user, result);
        return result;
    }
}