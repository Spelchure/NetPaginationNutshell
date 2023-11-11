using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PaginationExample.Model;

namespace PaginationExample.Controllers.Extensions;

public static class AddPaginationMetadataExtension
{
    public static void AddPaginationMetadata<T>(this ControllerBase controller, PagedList<T> pagedItems,
        PaginationQueryParameters queryParameters)
    {
        string? previousPageUrl = null;
        string? nextPageUrl = null;

        var paginationAttribute = (PaginatedHttpGetAttribute?)controller.ControllerContext.ActionDescriptor.MethodInfo
            .GetCustomAttributes(false).FirstOrDefault(obj => obj is PaginatedHttpGetAttribute);

        Debug.Assert(paginationAttribute is not null,
            "You should define PaginatedHttpGet attribute in paginated actions.");

        var routeName = paginationAttribute.Name;

        if (pagedItems.HasPrevious)
        {
            previousPageUrl = controller.Url.Link(routeName, queryParameters with
            {
                PageIndex = queryParameters.PageIndex - 1
            });
        }

        if (pagedItems.HasNext)
        {
            nextPageUrl = controller.Url.Link(routeName, queryParameters with
            {
                PageIndex = queryParameters.PageIndex + 1
            });
        }

        controller.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(
            new
            {
                pagedItems.HasNext,
                pagedItems.HasPrevious,
                pagedItems.TotalPageCount,
                pagedItems.TotalItemCount,
                pagedItems.CurrentPage,
                pagedItems.PageSize,
                previousPageUrl,
                nextPageUrl
            }));
    }
}