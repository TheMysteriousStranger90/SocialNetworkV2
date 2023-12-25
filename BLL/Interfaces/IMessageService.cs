using BLL.DTOs;
using BLL.Helpers;
using DAL.Specifications;

namespace BLL.Interfaces;

public interface IMessageService
{
    Task<MessageDto> CreateMessage(CreateMessageDto createMessageDto, string userName);
    Task<Pagination<MessageDto>> GetMessagesForUser(MessageParams messageParams, string userName);
    Task DeleteMessage(int id, string userName);
}