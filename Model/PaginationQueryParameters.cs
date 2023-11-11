namespace PaginationExample.Model;

public record PaginationQueryParameters(int PageIndex = 1, int PageSize = 10);