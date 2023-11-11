using Microsoft.AspNetCore.Mvc;
using PaginationExample.Database;
using PaginationExample.Controllers.Extensions;
using PaginationExample.Model;

namespace PaginationExample.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly ItemRepository _itemRepository;

    public ItemController(ItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    [PaginatedHttpGet("GetItems")]
    public async Task<ActionResult<List<Item>>> GetItems([FromQuery] PaginationQueryParameters paginationData)
    {
        var items = await _itemRepository.GetItems(paginationData.PageIndex, paginationData.PageSize);

        this.AddPaginationMetadata(items, paginationData);

        return items.ToList();
    }
}