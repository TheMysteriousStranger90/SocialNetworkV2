using BLL.DTOs;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Specifications;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Errors;
using WebAPI.Extensions;
using WebAPI.Helpers;

namespace WebAPI.Controllers;

[Authorize]
public class MessagesController : BaseApiController
{
    private readonly IMessageService _messageService;
    private readonly IValidator<CreateMessageDto> _createMessageValidator;
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(IMessageService messageService, IValidator<CreateMessageDto> createMessageValidator, ILogger<MessagesController> logger)
    {
        _messageService = messageService;
        _createMessageValidator = createMessageValidator;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new message.
    /// </summary>
    /// <param name="createMessageDto">The message details.</param>
    /// <returns>The created message.</returns>
    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
    {
        var userName = User.GetUserName();
        _logger.LogInformation("User {UserName} is attempting to create a message.", userName);

        var validationResult = await _createMessageValidator.ValidateAsync(createMessageDto);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for user {UserName} creating a message: {Errors}", userName, validationResult.Errors);
            return BadRequest(validationResult.CreateValidationProblemDetails(HttpContext));
        }

        try
        {
            var result = await _messageService.CreateMessage(createMessageDto, userName);
            _logger.LogInformation("Message created successfully by user {UserName}.", userName);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating message for user {UserName}.", userName);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves messages for the current user.
    /// </summary>
    /// <param name="messageParams">The message parameters.</param>
    /// <returns>A paginated list of messages.</returns>
    [HttpGet]
    public async Task<ActionResult<Pagination<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
    {
        var userName = User.GetUserName();
        _logger.LogInformation("User {UserName} is retrieving messages.", userName);

        try
        {
            var messages = await _messageService.GetMessagesForUser(messageParams, userName);
            Response.AddPaginationHeader(new PaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages));
            _logger.LogInformation("Messages retrieved successfully for user {UserName}.", userName);
            return messages;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving messages for user {UserName}.", userName);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a message.
    /// </summary>
    /// <param name="id">The message ID.</param>
    /// <returns>An action result.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMessage(int id)
    {
        var userName = User.GetUserName();
        _logger.LogInformation("User {UserName} is attempting to delete message with ID {MessageId}.", userName, id);

        try
        {
            await _messageService.DeleteMessage(id, userName);
            _logger.LogInformation("Message with ID {MessageId} deleted successfully by user {UserName}.", id, userName);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting message with ID {MessageId} for user {UserName}.", id, userName);
            return BadRequest(ex.Message);
        }
    }
}