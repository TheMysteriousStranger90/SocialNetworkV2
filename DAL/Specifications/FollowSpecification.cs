using DAL.Entities;

namespace DAL.Specifications;

public class FollowSpecification : BaseSpecification<Follow>
{
    public FollowSpecification(FollowParams followParams) 
        : base(f => (!followParams.FollowerId.HasValue || f.FollowerId == followParams.FollowerId) &&
                    (!followParams.FollowedId.HasValue || f.FollowedId == followParams.FollowedId))
    {
        ApplyPaging(followParams.PageSize * (followParams.PageNumber - 1), followParams.PageSize);
    }
}