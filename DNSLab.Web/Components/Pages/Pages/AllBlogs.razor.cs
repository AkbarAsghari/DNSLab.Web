using DNSLab.Shared.Enums;
using DNSLab.Web.API.Page;
using DNSLab.Web.API.Page.DTOs;
using DNSLab.Web.API.Page.Enums;
using DNSLab.Web.DTOs.Repositories.Page;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Pages;

partial class AllBlogs
{
    [Inject] IPageApiClient PageApiClient { get; set; }

    List<PageInfoResponse> _Pages { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var response = await PageApiClient.GetPagesByTypeAsync(PageType.Blog);

        if (response.IsSuccess)
        {
            _Pages = response.Value!;
        }
    }
}
