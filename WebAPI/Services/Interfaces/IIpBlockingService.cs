using System.Net;

namespace WebAPI.Services.Interfaces;

public interface IIpBlockingService
{
    bool IsBlocked(IPAddress ipAddress);
}