using DAL.Entities;

namespace DAL.Interfaces;

public interface IPhotoRepository
{
    Task<Photo> GetPhotoByIdAsync(int id);
    Task<Photo> GetPhotoByIdAndUserNameAsync(int id, string appUserName);
    void RemovePhoto(Photo photo);
    Task<IEnumerable<Photo>> GetUnapprovedPhotos();
}