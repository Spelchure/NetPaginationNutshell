using Microsoft.AspNetCore.Mvc;

namespace PaginationExample.Model;

public class PaginatedHttpGetAttribute : HttpGetAttribute
{
    public PaginatedHttpGetAttribute(string name)
    {
        Name = name;
    }
}