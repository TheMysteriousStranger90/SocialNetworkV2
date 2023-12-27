using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Specifications;

namespace BLL.Services;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MessageDto> CreateMessage(CreateMessageDto createMessageDto, string userName)
    {
        if (userName == createMessageDto.RecipientUsername.ToLower())
            throw new SocialNetworkException("You cannot send messages to yourself");

        var sender = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var recipient = await _unitOfWork.UserRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

        if (recipient == null) throw new SocialNetworkException("Not Found!");

        var message = new Message
        {
            Sender = sender,
            Recipient = recipient,
            SenderUsername = sender.UserName,
            RecipientUsername = recipient.UserName,
            Content = createMessageDto.Content
        };

        _unitOfWork.MessageRepository.AddMessage(message);

        await _unitOfWork.SaveAsync();
        return _mapper.Map<MessageDto>(message);
    }

    public async Task<Pagination<MessageDto>> GetMessagesForUser(MessageParams messageParams, string userName)
    {
        messageParams.UserName = userName;

        var query = _unitOfWork.MessageRepository.GetMessagesForUser(messageParams);

        var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

        return await Pagination<MessageDto>
            .CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
    }

    public async Task DeleteMessage(int id, string userName)
    {
        var message = await _unitOfWork.MessageRepository.GetMessage(id);

        if (message.SenderUsername != userName && message.RecipientUsername != userName)
            throw new SocialNetworkException("Unauthorized");

        if (message.SenderUsername == userName) message.SenderDeleted = true;
        if (message.RecipientUsername == userName) message.RecipientDeleted = true;

        if (message.SenderDeleted && message.RecipientDeleted)
        {
            _unitOfWork.MessageRepository.DeleteMessage(message);
        }
        
        await _unitOfWork.SaveAsync();
    }
}