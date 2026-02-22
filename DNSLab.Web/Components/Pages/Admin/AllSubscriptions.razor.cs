using DNSLab.Web.DTOs.Repositories.Subscription;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Admin;

partial class AllSubscriptions
{
    [Inject] ISubscriptionRepository _SubscriptionsRepository { get; set; }

    IEnumerable<SubscriptionDTO>? _Subscriptions;
    bool _Loading = false;
    protected override async Task OnInitializedAsync()
    {
        _Loading = true;
        _Subscriptions = await _SubscriptionsRepository.GetAllSubscribes();
        _Loading = false;
    }
}
