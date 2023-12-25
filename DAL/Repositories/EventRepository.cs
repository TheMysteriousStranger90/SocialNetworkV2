using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class EventRepository : GenericRepository<Event>, IEventRepository
{
    public EventRepository(SocialNetworkContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Event>> GetEventsByUserIdAsync(int userId)
    {
        return await _context.Events
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<EventParticipant>> GetParticipantsInEventAsync(int eventId)
    {
        var eventEntity = await _context.Events
            .Include(e => e.Participants)
            .FirstOrDefaultAsync(e => e.Id == eventId);

        return eventEntity?.Participants;
    }

    public async Task AddParticipantToEventAsync(int eventId, EventParticipant participant)
    {
        var eventEntity = await _context.Events
            .Include(e => e.Participants)
            .FirstOrDefaultAsync(e => e.Id == eventId);

        eventEntity?.Participants.Add(participant);

        await _context.SaveChangesAsync();
    }

    public async Task RemoveParticipantFromEventAsync(int eventId, int participantId)
    {
        var eventEntity = await _context.Events
            .Include(e => e.Participants)
            .FirstOrDefaultAsync(e => e.Id == eventId);

        var participant = eventEntity?.Participants.FirstOrDefault(p => p.Id == participantId);

        if (participant != null)
        {
            eventEntity.Participants.Remove(participant);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> EventContainsParticipantAsync(int eventId, int participantId)
    {
        var eventEntity = await _context.Events
            .Include(e => e.Participants)
            .FirstOrDefaultAsync(e => e.Id == eventId);

        return eventEntity?.Participants.Any(p => p.Id == participantId) ?? false;
    }
}