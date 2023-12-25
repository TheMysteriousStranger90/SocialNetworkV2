using Microsoft.EntityFrameworkCore;

namespace BLL.Helpers;

public class Pagination<T> : List<T>
{
    public Pagination(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        PageSize = pageSize;
        TotalCount = count;
        AddRange(items);
    }

    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public IReadOnlyList<T> Data { get; set; }
    
    
    public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source, 
        int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new Pagination<T>(items, count, pageNumber, pageSize);
    }
    
}