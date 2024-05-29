using Microsoft.EntityFrameworkCore;

namespace VehicleManagementSystem.Service;

public class PaginatedList<T> : List<T>
{
    private bool _hasPreviousPage;
    private bool _hasNextPage;
    
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }

    public PaginatedList()
    {
        
    }
    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        this.AddRange(items);
        
        HasPreviousPage = PageIndex > 1;
        HasNextPage = PageIndex < TotalPages;
    }


    public bool HasPreviousPage
    {
        get => _hasPreviousPage;
        set => _hasPreviousPage = value;
    }
    
    public bool HasNextPage
    {
        get => _hasNextPage;
        set => _hasNextPage = value;
    }
    

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}