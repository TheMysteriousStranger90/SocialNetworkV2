using AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Helpers;
using BLL.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BLL.Services;

public class PhotoService : IPhotoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly Cloudinary _cloudinary;

    public PhotoService(IOptions<CloudinarySettings> config, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

        var acc = new Account
        (
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );

        _cloudinary = new Cloudinary(acc);
    }

    public async Task<PhotoDto> GetPhotoByIdAndUserNameAsync(int id, string userName)
    {
        var photo = await _unitOfWork.PhotoRepository.GetPhotoByIdAndUserNameAsync(id, userName);
        if (photo == null) throw new SocialNetworkException("Photo does not exist");

        return _mapper.Map<PhotoDto>(photo);
    }

    public async Task<PhotoDto> GetPhotoByIdAsync(int id)
    {
        var photo = await _unitOfWork.PhotoRepository.GetPhotoByIdAsync(id);
        if (photo == null) throw new SocialNetworkException("Photo does not exist");

        return _mapper.Map<PhotoDto>(photo);
    }

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();

        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                Folder = "da-net7"
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        return uploadResult;
    }

    public async Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos()
    {
        var photos = await _unitOfWork.PhotoRepository.GetUnapprovedPhotos();
        if (photos == null) throw new SocialNetworkException("Photos does not exist");

        return _mapper.Map<IEnumerable<PhotoForApprovalDto>>(photos);
    }

    public async Task ApprovePhoto(int photoId)
    {
        var photo = await _unitOfWork.PhotoRepository.GetPhotoByIdAsync(photoId);

        if (photo == null) throw new SocialNetworkException("Photo does not exist");

        photo.IsApproved = true;

        var user = await _unitOfWork.UserRepository.GetUserByPhotoId(photoId);

        if (!user.Photos.Any(x => x.IsMain)) photo.IsMain = true;

        await _unitOfWork.SaveAsync();
        
    }

    public async Task DeletePhotoByIdAsync(int id)
    {
        var photo = await _unitOfWork.PhotoRepository.GetPhotoByIdAsync(id);
        if (photo == null) throw new SocialNetworkException("Photo does not exist");

        if (photo.PublicId != null)
        {
            var deleteParams = new DeletionParams(photo.PublicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            if (result.Result == "ok")
            {
                _unitOfWork.PhotoRepository.RemovePhoto(photo);
            }
        }
        else
        {
            _unitOfWork.PhotoRepository.RemovePhoto(photo);
        }

        await _unitOfWork.SaveAsync();
    }
    
    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);

        return await _cloudinary.DestroyAsync(deleteParams);
    }
}