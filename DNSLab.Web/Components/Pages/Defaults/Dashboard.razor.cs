using DNSLab.Shared.DTOs.ReverseProxy;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Defaults;

partial class Dashboard
{
    [Inject] IStaticRepository _StaticRepository { get; set; }
    [Inject] NavigationManager _NavigationManager { get; set; }

    int? ZoneCount { get; set; }
    int? RecordsCount { get; set; }
    int? DDNSsCount { get; set; }
    int? OnlineTunnelsCount { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ZoneCount = await _StaticRepository.GetZonesCount();
        RecordsCount = await _StaticRepository.GetRecordsCount();
        DDNSsCount = await _StaticRepository.GetDDNSsCount();
        OnlineTunnelsCount = await _StaticRepository.OnlineTunnelsCount();
    }
}
