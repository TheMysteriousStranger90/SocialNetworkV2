using System.Security.Claims;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Extensions;

namespace WebAPI.SignalR;

[Authorize]
public class VideoHub: Hub
{
    private readonly IUserService _userService;

    public VideoHub(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task OnConnectedAsync()
    {
        var username = Context.User.GetUserName();
        
        if (username != null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, username);
        }
        
        await base.OnConnectedAsync();
    }

    public async Task JoinVideoCallAsync(string peerId, string chatName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatName);
        await Clients.OthersInGroup(chatName).SendAsync("UserConnected", peerId, Context.ConnectionId);
    }

    public async Task CallUserAsync(string peerId, string username, string callerUsername)
    {
        var callerEmail = (await _userService.GetUserByUsernameAsync(callerUsername)).Email;
        await Clients.Group(username).SendAsync("AnswerCall", Context.ConnectionId, callerEmail);
    }

    public async Task AcceptUserCallAsync(string peerId, string username)
    {
        await Clients.Group(username).SendAsync("CallAccepted", peerId);
    }

    public async Task RejectUserCallAsync(string username)
    {
        await Clients.Group(username).SendAsync("CallRejected");
    }

    public async Task LeaveVideoCallAsync(string chatName)
    {
        await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}