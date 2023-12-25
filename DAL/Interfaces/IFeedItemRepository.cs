using DAL.Entities;

namespace DAL.Interfaces;

public interface IFeedItemRepository : IGenericRepository<FeedItem>
{
    Task<IEnumerable<FeedItem>> GetFeedItemsByUserIdAsync(int userId);
    Task<IEnumerable<FeedItem>> GetFeedItemsByUserIdOrderedAsync(int userId);
    Task<IEnumerable<FeedItem>> GetFeedItemsInDateRangeAsync(DateTime start, DateTime end);
}