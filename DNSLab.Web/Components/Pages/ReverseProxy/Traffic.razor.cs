using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DNSLab.Web.Components.Pages.ReverseProxy;

partial class Traffic
{
    [Inject] ISubscriptionRepository _SubscriptionRepository { get; set; }

    long? _AvailableTraffic, _UsedTraffic = 0;

    protected override async Task OnInitializedAsync()
    {
        _AvailableTraffic = await _SubscriptionRepository.GetReverseProxyClientAvailableTraffics();
        _UsedTraffic = await _SubscriptionRepository.GetReverseProxyClientUsedTraffic();
    }
}
