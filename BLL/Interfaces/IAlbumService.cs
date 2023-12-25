using BLL.DTOs;

namespace BLL.Interfaces;

public interface IAlbumService
{
    Task<IEnumerable<AlbumDto>> GetAlbumsByUserNameAsync(string userName);
    Task<IEnumerable<PhotoDto>> GetPhotosInAlbumAsync(int albumId);
    Task AddPhotoToAlbumAsync(int albumId, PhotoDto photoDto);
    Task RemovePhotoFromAlbumAsync(int albumId, int photoId);
    Task<bool> AlbumContainsPhotoAsync(int albumId, int photoId);
}