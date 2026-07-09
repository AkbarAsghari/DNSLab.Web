using DNSLab.Shared.DTOs.ReverseProxy;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Defaults;

partial class Dashboard
{
    [Inject] IStaticRepository _StaticRepository { get; set; }
    [Inject] IReverseProxyTrafficRepository _ReverseProxyTrafficRepository { get; set; }

    int? ZoneCount { get; set; }
    int? RecordsCount { get; set; }
    int? DDNSsCount { get; set; }
    IEnumerable<ReverseProxyTrafficDTO>? _Traffics { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ZoneCount = await _StaticRepository.GetZonesCount();
        RecordsCount = await _StaticRepository.GetRecordsCount();
        DDNSsCount = await _StaticRepository.GetDDNSsCount();
        _Traffics = await _ReverseProxyTrafficRepository.GetCurrentTraffics();
    }
}
