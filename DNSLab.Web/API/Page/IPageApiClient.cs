using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Page.DTOs;
using DNSLab.Web.API.Page.Enums;

namespace DNSLab.Web.API.Page;

public interface IPageApiClient
{
    Task<Result<Guid>> CreatePageAsync(CreatePageRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdatePageAsync(UpdatePageRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeletePageAsync(PageIdRequest request, CancellationToken cancellationToken = default);
    Task<Result<List<PageInfoResponse>>> GetAllPagesAsync(CancellationToken cancellationToken = default);
    Task<Result<List<string>>> GetAllPageUrlsAsync(CancellationToken cancellationToken = default);
    Task<Result<List<PageInfoResponse>>> GetPagesByTypeAsync(PageType type, CancellationToken cancellationToken = default);
    Task<Result<PageResponse>> GetPageAsync(PageIdRequest request, CancellationToken cancellationToken = default);
    Task<Result<PageResponse>> GetPageByUrlAsync(PageUrlRequest request, CancellationToken cancellationToken = default);
    Task<Result<List<PageType>>> GetPageTypesAsync(CancellationToken cancellationToken = default);
}
