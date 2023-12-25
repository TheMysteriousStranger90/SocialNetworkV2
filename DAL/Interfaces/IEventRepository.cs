using DAL.Entities;

namespace DAL.Interfaces;

public interface IEventRepository : IGenericRepository<Event>
{
    Task<IEnumerable<Event>> GetEventsByUserIdAsync(int userId);
    Task<IEnumerable<EventParticipant>> GetParticipantsInEventAsync(int eventId);
    Task AddParticipantToEventAsync(int eventId, EventParticipant participant);
    Task RemoveParticipantFromEventAsync(int eventId, int participantId);
    Task<bool> EventContainsParticipantAsync(int eventId, int participantId);
}