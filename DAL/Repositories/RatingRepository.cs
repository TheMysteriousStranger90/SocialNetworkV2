using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class RatingRepository : GenericRepository<Rating>, IRatingRepository
{
    public RatingRepository(SocialNetworkContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Rating>> GetRatingsByUserIdAsync(int userId)
    {
        return await _context.Ratings
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Rating>> GetRatingsByPhotoIdAsync(int photoId)
    {
        return await _context.Ratings
            .Where(r => r.PhotoId == photoId)
            .ToListAsync();
    }

    public async Task<double> GetAverageRatingForPhotoAsync(int photoId)
    {
        var ratings = await _context.Ratings
            .Where(r => r.PhotoId == photoId)
            .ToListAsync();

        if (!ratings.Any())
        {
            return 0;
        }

        return ratings.Average(r => r.Value);
    }

    public async Task AddRatingToPhotoAsync(int userId, int photoId, int value)
    {
        var rating = new Rating
        {
            UserId = userId,
            PhotoId = photoId,
            Value = value
        };

        await _context.Ratings.AddAsync(rating);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRatingAsync(int userId, int photoId, int value)
    {
        var rating = await _context.Ratings
            .FirstOrDefaultAsync(r => r.UserId == userId && r.PhotoId == photoId);

        if (rating != null)
        {
            rating.Value = value;
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<Rating> GetRatingForPhotoByUserAsync(int photoId, int userId)
    {
        return await _context.Ratings.FirstOrDefaultAsync(r => r.PhotoId == photoId && r.UserId == userId);
    }
}