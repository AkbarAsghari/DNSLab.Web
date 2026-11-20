using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Page.DTOs;
using DNSLab.Web.API.Page.Enums;

namespace DNSLab.Web.API.Page;

public class PageApiClient(IApiClient apiClient) : IPageApiClient
{
    const string _Prefix = "api/page/";

    public Task<Result<Guid>> CreatePageAsync(CreatePageRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync<CreatePageRequest, Guid>(
            $"{_Prefix}CreatePage",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> DeletePageAsync(PageIdRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.DeleteAsync(
            $"{_Prefix}CreatePage",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<List<PageInfoResponse>>> GetAllPagesAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<PageInfoResponse>>(
            $"{_Prefix}GetAllPages",
            options: null,
            cancellationToken: cancellationToken);
    }

    public Task<Result<List<string>>> GetAllPageUrlsAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<string>>(
            $"{_Prefix}GetAllPageUrls",
            options: null,
            cancellationToken: cancellationToken);
    }

    public Task<Result<PageResponse>> GetPageAsync(PageIdRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<PageIdRequest, PageResponse>(
            $"{_Prefix}GetPage",
            request,
            cancellationToken: cancellationToken);
    }

    public Task<Result<PageResponse>> GetPageByUrlAsync(PageUrlRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<PageUrlRequest, PageResponse>(
            $"{_Prefix}GetPageByUrl",
            request,
            cancellationToken: cancellationToken);
    }

    public Task<Result<List<PageInfoResponse>>> GetPagesByTypeAsync(PageType type, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<PageType, List<PageInfoResponse>>(
            $"{_Prefix}GetPagesByType?type={(int)type}",
            cancellationToken: cancellationToken);
    }

    public Task<Result<List<PageType>>> GetPageTypesAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<PageType>>(
            $"{_Prefix}GetPageTypes",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> UpdatePageAsync(UpdatePageRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync(
            $"{_Prefix}UpdatePage",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }
}
