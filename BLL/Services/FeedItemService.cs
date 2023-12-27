using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services;

public class FeedItemService : IFeedItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FeedItemService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<FeedItemDto>> GetFeedItemsByUserNameAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var feedItems = await _unitOfWork.FeedItemRepository.GetFeedItemsByUserIdAsync(user.Id);
        return _mapper.Map<IEnumerable<FeedItemDto>>(feedItems);
    }

    public async Task<IEnumerable<FeedItemDto>> GetFeedItemsInDateRangeAsync(DateTime start, DateTime end)
    {
        var feedItems = await _unitOfWork.FeedItemRepository.GetFeedItemsInDateRangeAsync(start, end);
        return _mapper.Map<IEnumerable<FeedItemDto>>(feedItems);
    }
}