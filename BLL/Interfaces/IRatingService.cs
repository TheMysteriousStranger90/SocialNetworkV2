using BLL.DTOs;

namespace BLL.Interfaces;

public interface IRatingService
{
    Task<IEnumerable<RatingDto>> GetRatingsByUserNameAsync(string userName);
    Task<IEnumerable<RatingDto>> GetRatingsByPhotoIdAsync(int photoId);
    Task<double> GetAverageRatingForPhotoAsync(int photoId);
    Task AddRatingToPhotoAsync(RatingDto ratingDto);
    Task UpdateRatingAsync(RatingDto ratingDto);
}