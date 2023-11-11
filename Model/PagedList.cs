using Microsoft.EntityFrameworkCore;

namespace PaginationExample.Model;

public class PagedList<T> : List<T>
{
    public int PageSize { get; private set; }
    public int CurrentPage { get; private set; }
    public int TotalItemCount { get; private set; }
    public int TotalPageCount { get; private set; }
    public bool HasPrevious => (CurrentPage > 1);
    public bool HasNext => (CurrentPage < TotalPageCount);

    private PagedList(List<T> items, int pageSize, int currentPage, int totalItemCount)
    {
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalItemCount = totalItemCount;
        TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
        AddRange(items);
    }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> items, int pageIndex, int pageSize)
    {
        var count = await items.CountAsync();
        var pagedItems = await items.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
        return new PagedList<T>(pagedItems, pageSize, pageIndex, count);
    }
}