namespace DAL.Specifications;

public class UserParams : PaginationParams
{
    public string CurrentUsername { get; set; }
    public string Gender { get; set; }
    public int MinAge { get; set; } = 18;
    public int MaxAge { get; set; } = 100;
    public string OrderBy { get; set; } = "lastActive";
    private string _search = "";

    public string Search
    {
        get => _search;
        set => _search = value.ToLower();
    }
}