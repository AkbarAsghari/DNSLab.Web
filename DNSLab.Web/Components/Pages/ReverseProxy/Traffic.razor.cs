using DNSLab.Shared.DTOs.ReverseProxy;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DNSLab.Web.Components.Pages.ReverseProxy;

partial class Traffic
{
    [Inject] IReverseProxyTrafficRepository _ReverseProxyTrafficRepository { get; set; }

    IEnumerable<ReverseProxyTrafficDTO>? _Traffics { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _Traffics = await _ReverseProxyTrafficRepository.GetCurrentTraffics();
    }
}
