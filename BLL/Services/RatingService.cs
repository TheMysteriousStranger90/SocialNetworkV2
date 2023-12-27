using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class RatingService : IRatingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RatingService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RatingDto>> GetRatingsByUserNameAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
        var ratings = await _unitOfWork.RatingRepository.GetRatingsByUserIdAsync(user.Id);
        return _mapper.Map<IEnumerable<RatingDto>>(ratings);
    }

    public async Task<IEnumerable<RatingDto>> GetRatingsByPhotoIdAsync(int photoId)
    {
        var ratings = await _unitOfWork.RatingRepository.GetRatingsByPhotoIdAsync(photoId);
        return _mapper.Map<IEnumerable<RatingDto>>(ratings);
    }

    public async Task<double> GetAverageRatingForPhotoAsync(int photoId)
    {
        return await _unitOfWork.RatingRepository.GetAverageRatingForPhotoAsync(photoId);
    }

    public async Task AddRatingToPhotoAsync(RatingDto ratingDto)
    {
        var rating = _mapper.Map<Rating>(ratingDto);
        await _unitOfWork.RatingRepository.AddRatingToPhotoAsync(rating.UserId, rating.PhotoId, rating.Value);
    }

    public async Task UpdateRatingAsync(RatingDto ratingDto)
    {
        await _unitOfWork.RatingRepository.UpdateRatingAsync(ratingDto.UserId, ratingDto.PhotoId, ratingDto.Value);
    }
}