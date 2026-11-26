namespace DNSLab.Web.API.Common.Pagination;

public record PaginationRequest(
    int Page = 1,
    int PageSize = 10)
{
    public int Skip => ((Page < 1 ? 1 : Page) - 1) * PageSize;
}