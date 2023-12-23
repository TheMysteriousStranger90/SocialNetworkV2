namespace DAL.Specifications;

public class LikesParams : PaginationParams
{
    public int AppUserId { get; set; }
    public string Predicate { get; set; }
}