using BLL.DTOs;

namespace BLL.Interfaces;

public interface IFeedItemService
{
    Task<IEnumerable<FeedItemDto>> GetFeedItemsByUserNameAsync(string userName);
    Task<IEnumerable<FeedItemDto>> GetFeedItemsInDateRangeAsync(DateTime start, DateTime end);
}