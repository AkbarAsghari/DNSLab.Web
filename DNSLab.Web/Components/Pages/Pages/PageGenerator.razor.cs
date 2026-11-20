using ApexCharts;
using DNSLab.Web.API.Page;
using DNSLab.Web.API.Page.DTOs;
using DNSLab.Web.DTOs.Repositories.Page;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;

namespace DNSLab.Web.Components.Pages.Pages;

partial class PageGenerator
{
    [Inject] IPageApiClient PageApiClient { get; set; }
    [Inject] IMemoryCache _MemoryCache { get; set; }

    [Parameter] public string? Url { get; set; }

    PageResponse? _Page;
    protected override async Task OnParametersSetAsync()
    {
        if (!String.IsNullOrEmpty(Url))
        {
            if (!_MemoryCache.TryGetValue(Url, out PageResponse? cachedResponse))
            {
                var response = await PageApiClient.GetPageByUrlAsync(
                    new PageUrlRequest(Url));

                if (response.IsSuccess)
                {
                    cachedResponse = response.Value!;
                    _MemoryCache.Set(Url, cachedResponse, TimeSpan.FromMinutes(5));
                }
            }

            _Page = cachedResponse;
        }
    }
}
