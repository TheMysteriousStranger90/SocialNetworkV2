using DAL.Entities;

namespace DAL.Interfaces;

public interface IAlbumRepository : IGenericRepository<Album>
{
    Task<IEnumerable<Album>> GetAlbumsByUserIdAsync(int userId);
    Task<IEnumerable<Photo>> GetPhotosInAlbumAsync(int albumId);
    Task AddPhotoToAlbumAsync(int albumId, Photo photo);
    Task RemovePhotoFromAlbumAsync(int albumId, int photoId);
    Task<bool> AlbumContainsPhotoAsync(int albumId, int photoId);
}