using DAL.Entities;

namespace DAL.Interfaces;

public interface ISpecializationRepository : IGenericRepository<Specialization>
{
    Task<IEnumerable<AppUser>> GetUsersBySpecializationIdAsync(int specializationId);
    Task<Specialization> GetSpecializationByUserIdAsync(int userId);
    Task AssignSpecializationToUserAsync(int userId, int specializationId);
    Task RemoveSpecializationFromUserAsync(int userId);
}