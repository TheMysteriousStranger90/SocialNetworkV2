using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Services.Interfaces;

namespace WebAPI.Filters;

public class IpBlockActionFilter : ActionFilterAttribute
{
    private readonly IIpBlockingService _ipBlockingService;

    public IpBlockActionFilter(IIpBlockingService ipBlockingService)
    {
        _ipBlockingService = ipBlockingService;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var remoteIp = context.HttpContext.Connection.RemoteIpAddress;

        if (remoteIp != null && _ipBlockingService.IsBlocked(remoteIp))
        {
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            return;
        }

        base.OnActionExecuting(context);
    }
}