using DAL.Entities;

namespace DAL.Specifications;

public class AppUserSpecification : BaseSpecification<AppUser>
{
    public AppUserSpecification(UserParams userParams, bool includeSpecialization = false) 
        : base(a => (string.IsNullOrEmpty(userParams.Search) || a.LastName.ToLower().Contains(userParams.Search)) &&
                    (!userParams.SpecializationId.HasValue || a.SpecializationId == userParams.SpecializationId))
    {
        if (includeSpecialization)
        {
            AddInclude(a => a.Specialization);
        }

        ApplyPaging(userParams.PageSize * (userParams.PageNumber - 1), userParams.PageSize);

        if (!string.IsNullOrEmpty(userParams.OrderBy))
        {
            switch (userParams.OrderBy)
            {
                case "created":
                    AddOrderByDescending(a => a.Created);
                    break;
                default:
                    AddOrderBy(a => a.LastActive);
                    break;
            }
        }
    }

    public AppUserSpecification(int id, bool includeSpecialization = false) : base(x => x.Id == id)
    {
        if (includeSpecialization)
        {
            AddInclude(a => a.Specialization);
        }
    }
}