using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
{
    public AlbumRepository(SocialNetworkContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Album>> GetAlbumsByUserIdAsync(int userId)
    {
        return await _context.Albums
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Photo>> GetPhotosInAlbumAsync(int albumId)
    {
        var album = await _context.Albums
            .Include(a => a.Photos)
            .FirstOrDefaultAsync(a => a.Id == albumId);

        return album?.Photos;
    }

    public async Task AddPhotoToAlbumAsync(int albumId, Photo photo)
    {
        var album = await _context.Albums
            .Include(a => a.Photos)
            .FirstOrDefaultAsync(a => a.Id == albumId);

        album?.Photos.Add(photo);
    }

    public async Task RemovePhotoFromAlbumAsync(int albumId, int photoId)
    {
        var album = await _context.Albums
            .Include(a => a.Photos)
            .FirstOrDefaultAsync(a => a.Id == albumId);

        var photo = album?.Photos.FirstOrDefault(p => p.Id == photoId);

        if (photo != null)
        {
            album.Photos.Remove(photo);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> AlbumContainsPhotoAsync(int albumId, int photoId)
    {
        var album = await _context.Albums
            .Include(a => a.Photos)
            .FirstOrDefaultAsync(a => a.Id == albumId);

        return album?.Photos.Any(p => p.Id == photoId) ?? false;
    }
}