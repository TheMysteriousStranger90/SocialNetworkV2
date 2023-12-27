using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class EventService : IEventService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EventService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<EventDto>> GetEventsByUserNameAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var events = await _unitOfWork.EventRepository.GetEventsByUserIdAsync(user.Id);
        return _mapper.Map<IEnumerable<EventDto>>(events);
    }

    public async Task<IEnumerable<EventParticipantDto>> GetParticipantsInEventAsync(int eventId)
    {
        var participants = await _unitOfWork.EventRepository.GetParticipantsInEventAsync(eventId);
        return _mapper.Map<IEnumerable<EventParticipantDto>>(participants);
    }

    public async Task AddParticipantToEventAsync(int eventId, EventParticipantDto participantDto)
    {
        var participant = _mapper.Map<EventParticipant>(participantDto);
        await _unitOfWork.EventRepository.AddParticipantToEventAsync(eventId, participant);
    }

    public async Task RemoveParticipantFromEventAsync(int eventId, int participantId)
    {
        await _unitOfWork.EventRepository.RemoveParticipantFromEventAsync(eventId, participantId);
    }

    public async Task<bool> EventContainsParticipantAsync(int eventId, int participantId)
    {
        return await _unitOfWork.EventRepository.EventContainsParticipantAsync(eventId, participantId);
    }
}