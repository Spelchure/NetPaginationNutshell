using PaginationExample.Model;

namespace PaginationExample.Database;

public class ItemRepository
{
    private readonly ItemDbContext _context;
    public ItemRepository(ItemDbContext context)
    {
        _context = context;
    }
    
    public async Task<PagedList<Item>> GetItems(int pageIndex, int pageSize)  
    {  
        return await PagedList<Item>.CreateAsync(_context.Items, pageIndex, pageSize);  
    }
}