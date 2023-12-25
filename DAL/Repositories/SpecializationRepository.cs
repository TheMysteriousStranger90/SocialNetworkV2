using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class SpecializationRepository : GenericRepository<Specialization>, ISpecializationRepository
{
    public SpecializationRepository(SocialNetworkContext context) : base(context)
    {
    }

    public async Task<IEnumerable<AppUser>> GetUsersBySpecializationIdAsync(int specializationId)
    {
        return await _context.Users
            .Where(u => u.SpecializationId == specializationId)
            .ToListAsync();
    }

    public async Task<Specialization> GetSpecializationByUserIdAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user != null ? user.Specialization : null;
    }

    public async Task AssignSpecializationToUserAsync(int userId, int specializationId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            user.SpecializationId = specializationId;
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveSpecializationFromUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            user.SpecializationId = null;
            await _context.SaveChangesAsync();
        }
    }
}