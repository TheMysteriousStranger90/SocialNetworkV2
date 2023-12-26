using BLL.DTOs;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces;

public interface IPhotoService
{
    Task<PhotoDto> GetPhotoByIdAndUserNameAsync(int id, string userName);
    Task<PhotoDto> GetPhotoByIdAsync(int id);
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos();
    Task ApprovePhoto(int photoId);
    Task DeletePhotoByIdAsync(int id);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}