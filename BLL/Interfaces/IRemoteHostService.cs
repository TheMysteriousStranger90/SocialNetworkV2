using System.Net;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces;

public interface IRemoteHostService
{
    public IPAddress? GetRemoteHostIpAddressUsingRemoteIpAddress(HttpContext httpContext);

    public IPAddress? GetRemoteHostIpAddressUsingXForwardedFor(HttpContext httpContext);

    public IPAddress? GetRemoteHostIpAddressUsingXRealIp(HttpContext httpContext);
}