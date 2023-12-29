using BLL.DTOs;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Controllers;

[Authorize]
public class MessagesController : BaseApiController
{
    private readonly IMessageService _messageService;

    public MessagesController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
    {
        var userName = User.GetUserName();
        var result = await _messageService.CreateMessage(createMessageDto, userName);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
    {
        var userName = User.GetUserName();
        var messages = await _messageService.GetMessagesForUser(messageParams, userName);

        Response.AddPaginationHeader(new PaginationHeader(messages.CurrentPage, messages.PageSize,
            messages.TotalCount, messages.TotalPages));

        return messages;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMessage(int id)
    {
        var userName = User.GetUserName();
        await _messageService.DeleteMessage(id, userName);

        return Ok();
    }
}