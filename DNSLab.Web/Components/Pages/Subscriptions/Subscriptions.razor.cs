using DNSLab.Web.DTOs.Repositories.Shared;
using DNSLab.Web.DTOs.Repositories.Subscription;
using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Subscriptions;

partial class Subscriptions
{
    [Inject] ISubscriptionRepository _SubscriptionsRepository { get; set; }

    IEnumerable<SubscriptionDTO>? _Subscriptions;
    bool _Loading = false;
    protected override async Task OnInitializedAsync()
    {
        _Loading = true;
        _Subscriptions = await _SubscriptionsRepository.GetSubscribes();
        _Loading = false;
    }
}
