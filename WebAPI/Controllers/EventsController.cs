using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
public class EventsController : BaseApiController
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEventsByUserName(string userName)
    {
        try
        {
            var events = await _eventService.GetEventsByUserNameAsync(userName);
            return Ok(events);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{eventId}/participants")]
    public async Task<ActionResult<IEnumerable<EventParticipantDto>>> GetParticipantsInEvent(int eventId)
    {
        try
        {
            var participants = await _eventService.GetParticipantsInEventAsync(eventId);
            return Ok(participants);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{eventId}/participants")]
    public async Task<ActionResult> AddParticipantToEvent(int eventId, EventParticipantDto participantDto)
    {
        try
        {
            await _eventService.AddParticipantToEventAsync(eventId, participantDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{eventId}/participants/{participantId}")]
    public async Task<ActionResult> RemoveParticipantFromEvent(int eventId, int participantId)
    {
        try
        {
            await _eventService.RemoveParticipantFromEventAsync(eventId, participantId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{eventId}/participants/{participantId}")]
    public async Task<ActionResult<bool>> EventContainsParticipant(int eventId, int participantId)
    {
        try
        {
            var containsParticipant = await _eventService.EventContainsParticipantAsync(eventId, participantId);
            return Ok(containsParticipant);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}