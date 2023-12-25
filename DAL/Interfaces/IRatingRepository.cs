using DAL.Entities;

namespace DAL.Interfaces;

public interface IRatingRepository : IGenericRepository<Rating>
{
    Task<IEnumerable<Rating>> GetRatingsByUserIdAsync(int userId);
    Task<IEnumerable<Rating>> GetRatingsByPhotoIdAsync(int photoId);
    Task<double> GetAverageRatingForPhotoAsync(int photoId);
    Task AddRatingToPhotoAsync(int userId, int photoId, int value);
    Task UpdateRatingAsync(int userId, int photoId, int value);
}