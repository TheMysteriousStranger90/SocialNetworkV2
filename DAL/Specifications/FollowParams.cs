namespace DAL.Specifications;

public class FollowParams : PaginationParams
{
    public int? FollowerId { get; set; }
    public int? FollowedId { get; set; }
}