using BLL.DTOs;

namespace BLL.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetEventsByUserNameAsync(string userName);
    Task<IEnumerable<EventParticipantDto>> GetParticipantsInEventAsync(int eventId);
    Task AddParticipantToEventAsync(int eventId, EventParticipantDto participantDto);
    Task RemoveParticipantFromEventAsync(int eventId, int participantId);
    Task<bool> EventContainsParticipantAsync(int eventId, int participantId);
}