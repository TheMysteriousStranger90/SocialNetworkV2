using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class FeedItemRepository : GenericRepository<FeedItem>, IFeedItemRepository
{
    public FeedItemRepository(SocialNetworkContext context) : base(context)
    {
    }

    public async Task<IEnumerable<FeedItem>> GetFeedItemsByUserIdAsync(int userId)
    {
        return await _context.FeedItems
            .Where(fi => fi.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<FeedItem>> GetFeedItemsByUserIdOrderedAsync(int userId)
    {
        return await _context.FeedItems
            .Where(fi => fi.UserId == userId)
            .OrderByDescending(fi => fi.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<FeedItem>> GetFeedItemsInDateRangeAsync(DateTime start, DateTime end)
    {
        return await _context.FeedItems
            .Where(fi => fi.CreatedAt >= start && fi.CreatedAt <= end)
            .ToListAsync();
    }
}