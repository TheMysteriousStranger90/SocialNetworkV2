using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly SocialNetworkContext _context;

    public PhotoRepository(SocialNetworkContext context)
    {
        _context = context;
    }
    
    public async Task<Photo> GetPhotoByIdAsync(int id)
    {
        return await _context.Photos
            .IgnoreQueryFilters()
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Photo> GetPhotoByIdAndUserNameAsync(int id, string appUserName)
    {
        var photo = await _context.Photos.IgnoreQueryFilters().SingleOrDefaultAsync(x => x.Id == id);
        
        var averageVote = 0.0;
        var userVote = 0;

        if(await _context.Ratings.AnyAsync( rating => rating.PhotoId == id)) 
        {
            averageVote = await _context.Ratings.Where(rating => rating.PhotoId == id)
                .AverageAsync(rating => rating.Value);

            var user = _context.Users.FirstOrDefault(x => x.UserName == appUserName);
                
            var userId = user.Id;
            var ratingDb = await _context.Ratings.FirstOrDefaultAsync(
                rating => rating.PhotoId == id && rating.UserId == userId);
            if(ratingDb != null)
            {
                userVote = ratingDb.Value;
            }
                
            photo.AverageVote = averageVote;
            photo.UserVote = userVote;
        }

        return photo;
    }
    
    public void RemovePhoto(Photo photo)
    {
        _context.Photos.Remove(photo);
    }

    public async Task<IEnumerable<Photo>> GetUnapprovedPhotos()
    {
        return await _context.Photos
            .IgnoreQueryFilters()
            .Where(p => p.IsApproved == false).ToListAsync();
    }
}