using DAL.Entities;

namespace BLL.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}