using System.Net;
using WebAPI.Services.Interfaces;

namespace WebAPI.Middleware;

public class IpBlockMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IIpBlockingService _ipBlockingService;

    public IpBlockMiddleware(RequestDelegate next, IIpBlockingService ipBlockingService)
    {
        _next = next;
        _ipBlockingService = ipBlockingService;
    }

    public async Task Invoke(HttpContext context)
    {
        var remoteIp = context.Connection.RemoteIpAddress;

        if (remoteIp != null && _ipBlockingService.IsBlocked(remoteIp))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return;
        }

        await _next.Invoke(context);
    }
}