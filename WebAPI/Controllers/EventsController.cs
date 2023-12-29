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
        var events = await _eventService.GetEventsByUserNameAsync(userName);
        return Ok(events);
    }

    [HttpGet("{eventId}/participants")]
    public async Task<ActionResult<IEnumerable<EventParticipantDto>>> GetParticipantsInEvent(int eventId)
    {
        var participants = await _eventService.GetParticipantsInEventAsync(eventId);
        return Ok(participants);
    }

    [HttpPost("{eventId}/participants")]
    public async Task<ActionResult> AddParticipantToEvent(int eventId, EventParticipantDto participantDto)
    {
        await _eventService.AddParticipantToEventAsync(eventId, participantDto);
        return Ok();
    }

    [HttpDelete("{eventId}/participants/{participantId}")]
    public async Task<ActionResult> RemoveParticipantFromEvent(int eventId, int participantId)
    {
        await _eventService.RemoveParticipantFromEventAsync(eventId, participantId);
        return Ok();
    }

    [HttpGet("{eventId}/participants/{participantId}")]
    public async Task<ActionResult<bool>> EventContainsParticipant(int eventId, int participantId)
    {
        var containsParticipant = await _eventService.EventContainsParticipantAsync(eventId, participantId);
        return Ok(containsParticipant);
    }
}